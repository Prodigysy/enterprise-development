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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetApplicationCountByRealEstateType), GetType().Name);
        try
        {
            var result = await service.GetApplicationCountByRealEstateType();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetApplicationCountByRealEstateType), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetApplicationCountByRealEstateType), GetType().Name, ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Возвращает клиентов, которые ищут недвижимость указанного типа
    /// </summary>
    [HttpGet("clients-searching-specific-type")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetClientsSearchingRealEstateType([FromQuery] RealEstateType type)
    {
        logger.LogInformation("{method} method of {controller} is called with {type}", nameof(GetClientsSearchingRealEstateType), GetType().Name, type);
        try
        {
            var result = await service.GetClientsSearchingRealEstateType(type);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetClientsSearchingRealEstateType), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetClientsSearchingRealEstateType), GetType().Name, ex);
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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetClientsWithMinimumAmountApplications), GetType().Name);
        try
        {
            var result = await service.GetClientsWithMinimumAmountApplications();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetClientsWithMinimumAmountApplications), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetClientsWithMinimumAmountApplications), GetType().Name, ex);
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
        logger.LogInformation("{method} method of {controller} is called: {start} - {end}", nameof(GetSellersWithApplicationsInPeriod), GetType().Name, start, end);
        try
        {
            var result = await service.GetSellersWithApplicationsInPeriod(start, end);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetSellersWithApplicationsInPeriod), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetSellersWithApplicationsInPeriod), GetType().Name, ex);
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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5ClientsByApplicationCount), GetType().Name);
        try
        {
            var result = await service.GetTop5ClientsByApplicationCount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5ClientsByApplicationCount), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5ClientsByApplicationCount), GetType().Name, ex);
            return StatusCode(500, ex.Message);
        }
    }
}