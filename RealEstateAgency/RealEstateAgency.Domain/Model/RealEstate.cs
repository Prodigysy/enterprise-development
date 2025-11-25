using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Domain.Model;

/// <summary>
/// Объект недвижимости в риелторском агентстве
/// </summary>
public class RealEstate
{
    /// <summary>
    /// Идентификатор объекта недвижимости
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип объекта недвижимости
    /// </summary>
    public required RealEstateType RealEstateType { get; set; }

    /// <summary>
    /// Назначение объекта недвижимости
    /// </summary>
    public required RealEstatePurpose RealEstatePurpose { get; set; }

    /// <summary>
    /// Кадастровый номер объекта
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Адрес объекта недвижимости
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Количество этажей
    /// </summary>
    public required int NumberOfFloors { get; set; }

    /// <summary>
    /// Общая площадь объекта
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Количество комнат
    /// </summary>
    public int? NumberOfRooms { get; set; }

    /// <summary>
    /// Высота потолков
    /// </summary>
    public double? CeilingHeight { get; set; }

    /// <summary>
    /// Этаж, на котором расположен объект
    /// </summary>
    public int? FloorNumber { get; set; }

    /// <summary>
    /// Наличие обременений
    /// </summary>
    public required bool HasEncumbrances { get; set; }
}