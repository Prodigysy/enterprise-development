using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Contracts;

/// <summary>
/// Сервис для выполнения аналитических запросов по недвижимости, клиентам и заявкам
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получение списка клиентов-продавцов, оставивших заявки за указанный период
    /// </summary>
    /// <param name="start">Начальная дата периода</param>
    /// <param name="end">Конечная дата периода</param>
    /// <returns>Список контрагентов</returns>
    public Task<IList<CounterpartyDto>> GetSellersWithApplicationsInPeriod(DateOnly start, DateOnly end);

    /// <summary>
    /// Получение топ-5 клиентов по количеству заявок, отдельно для покупки и продажи
    /// </summary>
    /// <returns>Словарь: тип заявки -> список топ-клиентов c количеством заявок</returns>
    public Task<Dictionary<ApplicationType, List<CounterpartyDto>>> GetTop5ClientsByApplicationCount();

    /// <summary>
    /// Получение количества заявок по каждому типу недвижимости
    /// </summary>
    /// <returns>Словарь: тип недвижимости -> количество заявок</returns>
    public Task<IDictionary<RealEstateType, int>> GetApplicationCountByRealEstateType();

    /// <summary>
    /// Получение клиентов, открывших заявки с минимальной суммой
    /// </summary>
    /// <returns>Список контрагентов</returns>
    public Task<IList<CounterpartyDto>> GetClientsWithMinimumAmountApplications();

    /// <summary>
    /// Получение клиентов, ищущих недвижимость указанного типа
    /// </summary>
    /// <param name="type">Тип недвижимости</param>
    /// <returns>Список контрагентов, отсортированных по ФИО</returns>
    public Task<IList<CounterpartyDto>> GetClientsSearchingRealEstateType(RealEstateType type);
}