using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using System.Text;
using System.Text.Json;

namespace RealEstateAgency.Infrastructure.RabbitMq;

/// <summary>
/// Фоновый consumer RabbitMQ который читает сообщения из очереди и создаёт заявки через IRealEstateApplicationService
/// </summary>
public class RealEstateAgencyRabbitMqConsumer(
    IConnection connection,
    IServiceScopeFactory scopeFactory,
    IOptions<RabbitMqOptions> options,
    ILogger<RealEstateAgencyRabbitMqConsumer> logger
) : BackgroundService
{
    /// <summary>
    /// Настройки RabbitMQ для consumer
    /// </summary>
    private readonly RabbitMqOptions _opts = options.Value;

    /// <summary>
    /// Канал RabbitMQ используемый для потребления сообщений
    /// </summary>
    private IChannel? _channel;

    /// <summary>
    /// Тег зарегистрированного consumer в RabbitMQ для корректной отмены подписки
    /// </summary>
    private string? _consumerTag;

    /// <summary>
    /// Инициализация канала и настройка prefetch для ограничения количества сообщений в обработке
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запуска сервиса</param>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: _opts.PrefetchCount, global: false, cancellationToken: cancellationToken);

        logger.LogInformation("RabbitMQ consumer starting. Queue={Queue}, Prefetch={Prefetch}", _opts.QueueName, _opts.PrefetchCount);

        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Основной цикл consumer который ожидает очередь и запускает обработку входящих сообщений
    /// </summary>
    /// <param name="stoppingToken">Токен остановки сервиса</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_channel is null)
            throw new InvalidOperationException("RabbitMQ channel was not created.");

        _channel = await WaitForQueueAsync(_opts.QueueName, stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                RealEstateApplicationCreateUpdateDto dto;
                try
                {
                    dto = JsonSerializer.Deserialize<RealEstateApplicationCreateUpdateDto>(json)
                          ?? throw new JsonException("Deserialized DTO is null.");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Invalid payload. Rejecting. DeliveryTag={DeliveryTag} Queue={Queue}", ea.DeliveryTag, _opts.QueueName);

                    await _channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
                    return;
                }

                using var scope = scopeFactory.CreateScope();

                var applicationService = scope.ServiceProvider.GetRequiredService<IRealEstateApplicationService>();
                var objectService = scope.ServiceProvider.GetRequiredService<IApplicationService<RealEstateDto, RealEstateCreateUpdateDto, int>>();
                var counterpartyService = scope.ServiceProvider.GetRequiredService<IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int>>();

                try
                {
                    _ = await counterpartyService.Get(dto.CounterpartyId);
                    _ = await objectService.Get(dto.RealEstateId);

                    await applicationService.Create(dto);

                    await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);

                    logger.LogInformation("Message processed OK. DeliveryTag={DeliveryTag} Queue={Queue} CounterpartyId={CounterpartyId} RealEstateId={RealEstateId}", ea.DeliveryTag, _opts.QueueName, dto.CounterpartyId, dto.RealEstateId);
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogWarning(ex, "Invalid references in message. Rejecting without requeue. DeliveryTag={DeliveryTag} CounterpartyId={CounterpartyId} RealEstateId={RealEstateId}", ea.DeliveryTag, dto.CounterpartyId, dto.RealEstateId);

                    await _channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Message processing failed. Nack. DeliveryTag={DeliveryTag} Queue={Queue} Requeue={Requeue}", ea.DeliveryTag, _opts.QueueName, _opts.RequeueOnError);

                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: _opts.RequeueOnError);
            }
        };

        _consumerTag = await _channel.BasicConsumeAsync(queue: _opts.QueueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);
    }

    /// <summary>
    /// Ожидание появления очереди без её создания чтобы consumer мог стартовать раньше продюсера
    /// </summary>
    /// <param name="queueName">Имя очереди которую должен создать продюсер</param>
    /// <param name="ct">Токен отмены ожидания</param>
    private async Task<IChannel> WaitForQueueAsync(string queueName, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            IChannel? channel = null;

            try
            {
                channel = await connection.CreateChannelAsync(cancellationToken: ct);

                await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: _opts.PrefetchCount, global: false, cancellationToken: ct);

                await channel.QueueDeclarePassiveAsync(queueName, ct);

                logger.LogInformation("RabbitMQ queue is available. Queue={Queue}", queueName);

                return channel;
            }
            catch (OperationInterruptedException ex) when (ex.ShutdownReason?.ReplyCode == 404)
            {
                logger.LogWarning("RabbitMQ queue not found yet. Waiting... Queue={Queue}, RetryDelaySec={Delay}", queueName, _opts.QueueWaitRetryDelaySeconds);

                if (channel is not null)
                {
                    try { await channel.DisposeAsync(); } catch { }
                }

                await Task.Delay(TimeSpan.FromSeconds(_opts.QueueWaitRetryDelaySeconds), ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RabbitMQ error while waiting for queue. Queue={Queue}, RetryDelaySec={Delay}", queueName, _opts.QueueWaitRetryDelaySeconds);

                if (channel is not null)
                {
                    try { await channel.DisposeAsync(); } catch { }
                }

                await Task.Delay(TimeSpan.FromSeconds(_opts.QueueWaitRetryDelaySeconds), ct);
            }
        }

        throw new OperationCanceledException(ct);
    }

    /// <summary>
    /// Остановка consumer, отмена подписки и корректное закрытие канала
    /// </summary>
    /// <param name="cancellationToken">Токен остановки сервиса</param>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_channel is not null && _consumerTag is not null)
                await _channel.BasicCancelAsync(_consumerTag, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error cancelling consumer.");
        }

        try
        {
            if (_channel is not null)
            {
                await _channel.CloseAsync(cancellationToken: cancellationToken);
                await _channel.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error closing RabbitMQ channel.");
        }

        logger.LogInformation("RabbitMQ consumer stopped. Queue={Queue}", _opts.QueueName);

        await base.StopAsync(cancellationToken);
    }
}