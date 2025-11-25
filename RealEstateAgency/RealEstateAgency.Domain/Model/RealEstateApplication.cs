using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Domain.Model;

/// <summary>
/// Заявка контрагента на объект недвижимости
/// </summary>
public class RealEstateApplication
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public required int Id { get; set; }

    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Контрагент
    /// </summary>
    public Counterparty? Counterparty { get; set; }

    public required int RealEstateId { get; set; }

    /// <summary>
    /// Объект недвижимости
    /// </summary>
    public RealEstate? RealEstate { get; set; }

    /// <summary>
    /// Тип заявки
    /// </summary>
    public required ApplicationType ApplicationType { get; set; }

    /// <summary>
    /// Сумма заявки
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Дата создания заявки
    /// </summary>
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
