using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Application.Contracts.RealEstateApplication;

namespace RealEstateAgency.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления заявками на объекты недвижимости;
/// Предоставляет стандартные CRUD-операции, а также методы получения
/// связанного объекта недвижимости и контрагента по идентификатору заявки
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RealEstateApplicationController(IRealEstateApplicationService appService, ILogger<RealEstateApplicationController> logger) 
    : CrudControllerBase<RealEstateApplicationDto, RealEstateApplicationCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Получает объект недвижимости, связанный с указанной заявкой
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>
    /// Возвращает DTO объекта недвижимости, 
    /// либо 404 если заявка или объект недвижимости не найдены,
    /// либо 500 при ошибке сервера
    /// </returns>
    [HttpGet("{id}/RealEstate")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<RealEstateDto>> GetRealEstate(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetRealEstate), GetType().Name, id);
        try
        {
            var res = await appService.GetRealEstate(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetRealEstate), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetRealEstate), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetRealEstate), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получает контрагента, связанного с указанной заявкой
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>
    /// Возвращает DTO контрагента, 
    /// либо 404 если заявка или контрагент не найдены,
    /// либо 500 при ошибке сервера
    /// </returns>
    [HttpGet("{id}/Counterparty")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<RealEstateDto>> GetCounterparty(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetCounterparty), GetType().Name, id);
        try
        {
            var res = await appService.GetCounterparty(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetCounterparty), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetCounterparty), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetCounterparty), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}