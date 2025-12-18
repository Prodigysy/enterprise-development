using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using RealEstateAgency.Generator.RabbitMq.Host.Generator;

namespace RealEstateAgency.Generator.RabbitMq.Host.Controllers;

/// <summary>
/// Контроллер для генерации тестовых DTO заявок и отправки их батчами в RabbitMQ
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(ILogger<GeneratorController> logger, RealEstateAgencyRabbitMqProducer producerService) : ControllerBase
{
    /// <summary>
    /// Генерирует payloadLimit DTO и отправляет их в RabbitMQ батчами по batchSize с задержкой waitTime секунд между отправками
    /// </summary>
    /// <param name="batchSize">Размер одной партии сообщений</param>
    /// <param name="payloadLimit">Общее количество сообщений для генерации и отправки</param>
    /// <param name="waitTime">Задержка между батчами в секундах</param>
    /// <returns>Список сгенерированных DTO</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<RealEstateApplicationCreateUpdateDto>>> Get([FromQuery] int batchSize, [FromQuery] int payloadLimit, [FromQuery] int waitTime, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating {limit} contracts via {batchSize} batches and {waitTime}s delay", payloadLimit, batchSize, waitTime);
        try
        {
            var list = new List<RealEstateApplicationCreateUpdateDto>(payloadLimit);
            var sent = 0;
            while (sent < payloadLimit)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var currentBatchSize = Math.Min(batchSize, payloadLimit - sent);

                var batch = RealEstateApplicationGenerator.GenerateLinks(currentBatchSize);

                await producerService.SendAsync(batch);
                logger.LogInformation("Batch of {batchSize} items has been sent", batchSize);
                
                sent += batchSize;

                list.AddRange(batch);

                if (waitTime > 0 && sent < payloadLimit)
                    await Task.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken);
            }

            logger.LogInformation("{Method} method of {Controller} executed successfully PayloadLimit={PayloadLimit} BatchSize={BatchSize}", nameof(Get), GetType().Name, payloadLimit, batchSize);
            return Ok(list);
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("{Method} method of {Controller} was cancelled by client", nameof(Get), GetType().Name);

            return StatusCode(499, "Request was cancelled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}