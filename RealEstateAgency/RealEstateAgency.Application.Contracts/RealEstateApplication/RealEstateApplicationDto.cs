using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Contracts.RealEstateApplication;

/// <summary>
/// DTO для отображения заявки на недвижимость
/// Содержит сведения о контрагенте, объекте недвижимости, типе заявки, сумме и дате создания
/// </summary>
/// <param name="CounterpartyId">Идентификатор контрагента, подавшего заявку</param>
/// <param name="RealEstateId">Идентификатор объекта недвижимости</param>
/// <param name="ApplicationType">Тип заявки</param>
/// <param name="Amount">Сумма сделки</param>
/// <param name="DateCreated">Дата создания заявки</param>
public record RealEstateApplicationDto(int CounterpartyId, int RealEstateId, ApplicationType ApplicationType, decimal? Amount, DateOnly DateCreated);