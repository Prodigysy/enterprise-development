using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Contracts.RealEstateApplication;

/// <summary>
/// DTO для создания или обновления заявки на недвижимость
/// Содержит идентификаторы контрагента, объекта недвижимости, тип заявки, сумму и дату создания
/// </summary>
/// <param name="CounterpartyId">Идентификатор контрагента, подавшего заявку</param>
/// <param name="RealEstateId">Идентификатор объекта недвижимости</param>
/// <param name="ApplicationType">Тип заявки: покупка или продажа</param>
/// <param name="Amount">Сумма сделки</param>
/// <param name="DateCreated">Дата создания заявки</param>
public record RealEstateApplicationCreateUpdateDto(int CounterpartyId, int RealEstateId, ApplicationType ApplicationType, decimal? Amount, DateOnly DateCreated);