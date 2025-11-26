using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Application.Contracts.RealEstate;

namespace RealEstateAgency.Application.Contracts.RealEstateApplication;

/// <summary>
/// Сервис для работы с заявками на недвижимость;
/// Расширяет базовый CRUD-интерфейс дополнительными операциями 
/// получения связанных сущностей: контрагента и объекта недвижимости
/// </summary>
public interface IRealEstateApplicationService : IApplicationService<RealEstateApplicationDto, RealEstateApplicationCreateUpdateDto, int>
{
    // <summary>
    /// Возвращает контрагента, связанного с заявкой
    /// </summary>
    public Task<CounterpartyDto> GetCounterparty(int id);

    /// <summary>
    /// Возвращает объект недвижимости, связанный с заявкой
    /// </summary>
    public Task<RealEstateDto> GetRealEstate(int id);
}