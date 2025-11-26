namespace RealEstateAgency.Application.Contracts.Counterparty;

/// <summary>
/// DTO для создания или обновления контрагента
/// Содержит паспортные данные, имя, контакты и при необходимости отчество
/// </summary>
/// <param name="PassportNumber">Паспортные данные</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество при наличии</param>
/// <param name="LastName">Фамилия</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record CounterpartyCreateUpdateDto(string PassportNumber, string FirstName, string? Patronymic, string LastName, string PhoneNumber);