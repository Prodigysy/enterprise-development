using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Application.Contracts.RealEstate;

/// <summary>
/// DTO для создания или обновления объекта недвижимости
/// Содержит основные характеристики объекта такие как тип, назначение, адрес, площадь и дополнительные параметры
/// </summary>
/// <param name="RealEstateType">Тип недвижимости</param>
/// <param name="RealEstatePurpose">Назначение недвижимости</param>
/// <param name="CadastralNumber">Кадастровый номер</param>
/// <param name="Address">Адрес объекта</param>
/// <param name="NumberOfFloors">Количество этажей</param>
/// <param name="TotalArea">Общая площадь</param>
/// <param name="NumberOfRooms">Количество комнат</param>
/// <param name="CeilingHeight">Высота потолков</param>
/// <param name="FloorNumber">Этаж</param>
/// <param name="HasEncumbrances">Наличие обременений</param>
public record RealEstateCreateUpdateDto(RealEstateType RealEstateType, RealEstatePurpose RealEstatePurpose, string CadastralNumber, string Address, int NumberOfFloors, double TotalArea, int? NumberOfRooms, double? CeilingHeight, int? FloorNumber, bool HasEncumbrances);