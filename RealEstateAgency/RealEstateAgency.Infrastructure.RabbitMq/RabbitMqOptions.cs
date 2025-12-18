namespace RealEstateAgency.Infrastructure.RabbitMq;

/// <summary>
/// Настройки RabbitMQ для consumer
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Имя очереди из которой consumer читает сообщения
    /// </summary>
    public required string QueueName { get; init; }

    /// <summary>
    /// Количество сообщений которые consumer может держать в обработке одновременно
    /// Значение передаётся в BasicQos как prefetchCount
    /// </summary>
    public ushort PrefetchCount { get; init; } = 10;

    /// <summary>
    /// Поведение при ошибке обработки сообщения
    /// True возвращает сообщение в очередь, False делает Nack без возврата
    /// </summary>
    public bool RequeueOnError { get; init; } = false;

    /// <summary>
    /// Пауза между попытками проверить наличие очереди при старте consumer
    /// </summary>
    public int QueueWaitRetryDelaySeconds { get; init; } = 5;
}