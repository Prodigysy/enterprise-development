using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.RealEstate;

namespace RealEstateAgency.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления объектами недвижимости;
/// Предоставляет стандартный набор CRUD-операций
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RealEstateController(IApplicationService<RealEstateDto, RealEstateCreateUpdateDto, int> appService, ILogger<RealEstateController> logger) 
    : CrudControllerBase<RealEstateDto, RealEstateCreateUpdateDto, int>(appService, logger);