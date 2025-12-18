using Bogus;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Generator.RabbitMq.Host.Generator;

/// <summary>
/// Генератор тестовых DTO заявок на недвижимость для публикации в RabbitMQ
/// </summary>
public static class RealEstateApplicationGenerator
{
    /// <summary>
    /// Генерирует список заявок RealEstateApplicationCreateUpdateDto заданного размера
    /// </summary>
    /// <param name="count">Количество генерируемых DTO</param>
    /// <returns>Список DTO для создания заявок</returns>
    public static List<RealEstateApplicationCreateUpdateDto> GenerateLinks(int count) =>
        new Faker<RealEstateApplicationCreateUpdateDto>()
            .WithRecord()
            .RuleFor(x => x.CounterpartyId, f => f.Random.Int(1, 20))
            .RuleFor(x => x.RealEstateId, f => f.Random.Int(1, 20))
            .RuleFor(x => x.ApplicationType, f => f.PickRandom<ApplicationType>())
            .RuleFor(x => x.Amount, f =>
            {
                var type = f.PickRandom<ApplicationType>();
                var amount = f.Random.Bool(0.9f)
                    ? Math.Round(f.Random.Decimal(1_000_000m, 15_000_000m), 2)
                    : (decimal?)null;

                return type == ApplicationType.Purchase ? amount ?? Math.Round(f.Random.Decimal(1_000_000m, 15_000_000m), 2) : amount;
            })
            .RuleFor(x => x.DateCreated, f => DateOnly.FromDateTime(f.Date.Past(1, DateTime.Today)))
            .Generate(count);
}