namespace RealEstateAgency.Domain.Model;

/// <summary>
/// Контрагент риелторского агентства
/// </summary>
public class Counterparty
{
    /// <summary>
    /// Идентификатор контрагента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Серия и номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя контрагента
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество контрагента
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Фамилия контрагента
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Контактный номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}