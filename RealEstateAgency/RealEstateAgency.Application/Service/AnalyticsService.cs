using AutoMapper;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Model;
using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Service;

/// <summary>
/// Сервис предоставляет аналитические операции над заявками, объектами недвижимости
/// и контрагентами; Выполняет группировки, фильтрации и агрегирующие запросы,
/// используя репозитории доменных сущностей и AutoMapper
/// </summary>
public class AnalyticsService(
    IRepository<Counterparty, int> counterpartyRepository, 
    IRepository<RealEstateApplication, int> applicationRepository,
    IRepository<RealEstate, int> realEstateRepository,
    IMapper mapper
) : IAnalyticsService
{
    /// <summary>
    /// Возвращает количество заявок, сгруппированных по типу недвижимости
    /// </summary>
    /// <returns>
    /// Словарь, где ключ - тип недвижимости, значение - количество заявок
    /// </returns>
    public async Task<IDictionary<RealEstateType, int>> GetApplicationCountByRealEstateType()
    {
        var applications = await applicationRepository.GetAll();
        var estates = await realEstateRepository.GetAll();

        return applications
            .GroupBy(a => estates.First(r => r.Id == a.RealEstateId).RealEstateType)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    /// <summary>
    /// Возвращает список клиентов, подавших заявки на выбранный тип недвижимости
    /// </summary>
    /// <param name="type">Тип недвижимости для фильтрации</param>
    /// <returns>
    /// Отсортированный список DTO клиентов, ищущих недвижимость указанного типа
    /// </returns>
    public async Task<IList<CounterpartyDto>> GetClientsSearchingRealEstateType(RealEstateType type)
    {
        var applications = await applicationRepository.GetAll();
        var estates = await realEstateRepository.GetAll();
        var clients = await counterpartyRepository.GetAll();

        var ids = applications
            .Where(a => estates.First(r => r.Id == a.RealEstateId).RealEstateType == type)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        return [.. clients
            .Where(c => ids.Contains(c.Id))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Возвращает клиентов, чьи заявки имеют минимальную сумму среди всех заявок
    /// </summary>
    /// <returns>Список DTO контрагентов с минимальной суммой заявки</returns>
    public async Task<IList<CounterpartyDto>> GetClientsWithMinimumAmountApplications()
    {
        var applications = await applicationRepository.GetAll();
        var clients = await counterpartyRepository.GetAll();

        var minAmount = applications.Min(a => a.Amount);

        var ids = applications
            .Where(a => a.Amount == minAmount)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        return [.. clients
            .Where(c => ids.Contains(c.Id))
            .Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Возвращает продавцов, подавших заявки в заданный период времени,
    /// Учитываются только заявки на продажу
    /// </summary>
    /// <param name="start">Начальная дата</param>
    /// <param name="end">Конечная дата</param>
    /// <returns>Список DTO контрагентов-продавцов</returns>
    public async Task<IList<CounterpartyDto>> GetSellersWithApplicationsInPeriod(DateOnly start, DateOnly end)
    {
        var applications = await applicationRepository.GetAll();
        var clients = await counterpartyRepository.GetAll();

        var ids = applications
            .Where(a => a.ApplicationType == ApplicationType.Sale)
            .Where(a => a.DateCreated >= start && a.DateCreated <= end)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        return [.. clients
            .Where(c => ids.Contains(c.Id))
            .Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Возвращает топ-5 клиентов по количеству заявок для каждого типа заявки
    /// </summary>
    /// <returns>
    /// Словарь: ключ - тип заявки, значение - список из пяти клиентов с
    /// наибольшим количеством заявок соответствующего типа
    /// </returns>
    public async Task<Dictionary<ApplicationType, List<CounterpartyDto>>> GetTop5ClientsByApplicationCount()
    {
        var applications = await applicationRepository.GetAll();
        var clients = await counterpartyRepository.GetAll();

        var grouped = applications
            .GroupBy(a => new { a.ApplicationType, a.CounterpartyId })
            .Select(g => new
            {
                g.Key.ApplicationType,
                ClientId = g.Key.CounterpartyId,
                Count = g.Count()
            })
            .GroupBy(x => x.ApplicationType)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(x => x.Count)
                      .Take(5)
                      .Select(x => clients.First(c => c.Id == x.ClientId))
                      .Select(mapper.Map<CounterpartyDto>)
                      .ToList()
            );

        return grouped;
    }
}