namespace RealEstateAgency.Domain.Enum;

/// <summary>
/// Назначение объекта недвижимости
/// </summary>
public enum RealEstatePurpose
{
    /// <summary>
    /// Жилое назначение
    /// </summary>
    Residential,

    /// <summary>
    /// Коммерческое назначение
    /// </summary>
    Commercial,

    /// <summary>
    /// Производственное / складское назначение
    /// </summary>
    Industrial,

    /// <summary>
    /// Сельскохозяйственное назначение
    /// </summary>
    Agricultural,

    /// <summary>
    /// Смешанное использование
    /// </summary>
    Mixed
}