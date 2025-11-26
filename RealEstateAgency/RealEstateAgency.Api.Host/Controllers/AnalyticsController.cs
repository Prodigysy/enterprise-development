using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Api.Host.Controllers;

/// <summary>
/// Контроллер, предоставляющий доступ к аналитическим операциям:
/// статистика заявок, выборки клиентов и продавцов по условиям,
/// топы по количеству заявок
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : Controller
{
    /// <summary>
    /// Возвращает количество заявок, сгруппированных по типу недвижимости
    /// </summary>
    [HttpGet("applications-by-real-estate-type")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetApplicationCountByRealEstateType()
    {
        logger.LogInformation("GetApplicationCountByRealEstateType called");
        try
        {
            var result = await service.GetApplicationCountByRealEstateType();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetApplicationCountByRealEstateType");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Возвращает клиентов, которые ищут недвижимость указанного типа
    /// </summary>
    [HttpGet("clients-searching-{type}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetClientsSearchingRealEstateType(RealEstateType type)
    {
        logger.LogInformation("GetClientsSearchingRealEstateType called with {type}", type);
        try
        {
            var result = await service.GetClientsSearchingRealEstateType(type);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetClientsSearchingRealEstateType");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Возвращает клиентов с минимальной суммой заявки
    /// </summary>
    [HttpGet("clients-with-min-amount")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetClientsWithMinimumAmountApplications()
    {
        logger.LogInformation("GetClientsWithMinimumAmountApplications called");
        try
        {
            var result = await service.GetClientsWithMinimumAmountApplications();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetClientsWithMinimumAmountApplications");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Возвращает продавцов, подавших заявки в указанный период
    /// </summary>
    [HttpGet("sellers-in-specific-period")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetSellersWithApplicationsInPeriod([FromQuery] DateOnly start, [FromQuery] DateOnly end)
    {
        logger.LogInformation("GetSellersWithApplicationsInPeriod called: {start} - {end}", start, end);
        try
        {
            var result = await service.GetSellersWithApplicationsInPeriod(start, end);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetSellersWithApplicationsInPeriod");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Возвращает топ-5 клиентов по количеству заявок для каждого типа заявки
    /// </summary>
    [HttpGet("clients-top5-by-application-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetTop5ClientsByApplicationCount()
    {
        logger.LogInformation("GetTop5ClientsByApplicationCount called");
        try
        {
            var result = await service.GetTop5ClientsByApplicationCount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetTop5ClientsByApplicationCount");
            return StatusCode(500, ex.Message);
        }
    }
}