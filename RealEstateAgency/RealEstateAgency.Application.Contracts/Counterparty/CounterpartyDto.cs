namespace RealEstateAgency.Application.Contracts.Counterparty;

/// <summary>
/// DTO для отображения информации о контрагенте
/// Содержит идентификатор, паспортные данные, ФИО и телефон
/// </summary>
/// <param name="Id">Идентификатор контрагента</param>
/// <param name="PassportNumber">Паспортные данные</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="LastName">Фамилия</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record CounterpartyDto(int Id, string PassportNumber, string FirstName, string? Patronymic, string LastName, string PhoneNumber);