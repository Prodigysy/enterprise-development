using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Contracts.RealEstateApplication;

/// <summary>
/// DTO для создания или обновления заявки на недвижимость
/// Содержит идентификатор заявки тип заявки сумму и дату создания
/// </summary>
/// <param name="Id">Идентификатор заявки</param>
/// <param name="ApplicationType">Тип заявки: покупка или продажа</param>
/// <param name="Amount">Сумма сделки</param>
/// <param name="DateCreated">Дата создания заявки</param>
public record RealEstateApplicationCreateUpdateDto(int Id, ApplicationType ApplicationType, decimal? Amount, DateOnly DateCreated);