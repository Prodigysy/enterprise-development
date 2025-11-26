using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.Counterparty;

namespace RealEstateAgency.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления контрагентами;
/// Предоставляет стандартный набор CRUD-операций
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CounterpartyController(IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int> appService, ILogger<CounterpartyController> logger) 
    : CrudControllerBase<CounterpartyDto, CounterpartyCreateUpdateDto, int>(appService, logger);