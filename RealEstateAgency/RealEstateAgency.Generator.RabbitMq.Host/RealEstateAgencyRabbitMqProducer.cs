using RabbitMQ.Client;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using System.Text;
using System.Text.Json;

namespace RealEstateAgency.Generator.RabbitMq.Host;

/// <summary>
/// Имплементация для отправки контрактов через очередь RabbitMq
/// Создаёт очередь на стороне продюсера и публикует сообщения в default exchange с routingKey равным имени очереди
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="rabbitMqConnection">Подключение к брокеру сообщений</param>
/// <param name="logger">Логгер</param>
public class RealEstateAgencyRabbitMqProducer(
    IConfiguration configuration,
    IConnection rabbitMqConnection,
    ILogger<RealEstateAgencyRabbitMqProducer> logger)
{
    /// <summary>
    /// Имя очереди в которую публикуются сообщения
    /// </summary>
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    /// <summary>
    /// Отправляет список DTO в RabbitMQ как отдельные сообщения в формате JSON
    /// </summary>
    /// <param name="batch">Пакет сообщений для отправки</param>
    public async Task SendAsync(IList<RealEstateApplicationCreateUpdateDto> batch)
    {
        if (batch is null || batch.Count == 0)
            return;

        await using var channel = await rabbitMqConnection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var props = new BasicProperties
        {
            ContentType = "application/json",
            ContentEncoding = "utf-8",
            DeliveryMode = DeliveryModes.Persistent
        };

        foreach (var dto in batch)
        {
            var json = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "", routingKey: _queueName, mandatory: false, basicProperties: props, body: body);
        }

        logger.LogInformation("Published batch to RabbitMQ. Queue={Queue} Count={Count}", _queueName, batch.Count);
    }
}