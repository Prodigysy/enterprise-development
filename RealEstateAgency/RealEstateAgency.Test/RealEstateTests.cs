using RealEstateAgency.Domain.Data;
using RealEstateAgency.Domain.Shared.Enum;

namespace RealEstateAgency.Test;
public class RealEstateTests(RealEstateSeeder seeder): IClassFixture<RealEstateSeeder>
{
    /// <summary>
    /// Вывод всех продавцов, оставивших заявки за заданный период
    /// </summary>
    [Fact]
    public void Sellers_WithApplications_InGivenPeriod()
    {
        var start = DateOnly.FromDateTime(DateTime.Now).AddDays(-11);
        var end = DateOnly.FromDateTime(DateTime.Now);

        var sellers = seeder.Applications
            .Where(a => a.ApplicationType == ApplicationType.Sale)
            .Where(a => a.DateCreated >= start && a.DateCreated <= end)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains(4, sellers);
        Assert.Contains(10, sellers);
        Assert.Equal(2, sellers.Count);
    }

    /// <summary>
    /// Вывод топ 5 клиентов по количеству заявок отдельно на покупку и продажу
    /// </summary>
    [Fact]
    public void Top5Clients_ByApplicationCount()
    {
        var topClients = seeder.Applications
        .GroupBy(a => new { a.ApplicationType, a.CounterpartyId })
        .Select(g => new
        {
            g.Key.ApplicationType,
            ClientId = g.Key.CounterpartyId,
            Count = g.Count()
        })
        .GroupBy(x => x.ApplicationType)
        .ToDictionary(
            g => g.Key,
            g => g.OrderByDescending(x => x.Count).Take(5).ToList()
        );

        Assert.NotEmpty(topClients);

        var buyers = topClients[ApplicationType.Purchase];
        var sellers = topClients[ApplicationType.Sale];

        Assert.True(buyers.Count <= 5);
        Assert.True(sellers.Count <= 5);

        Assert.Contains(buyers, c => c.ClientId == 1);
        Assert.Contains(buyers, c => c.ClientId == 3);
        Assert.Contains(sellers, c => c.ClientId == 4);
        Assert.Contains(sellers, c => c.ClientId == 10);
    }

    /// <summary>
    /// Вывод информации о количестве заявок по каждому типу недвижимости
    /// </summary>
    [Fact]
    public void ApplicationCount_ByRealEstateType()
    {
        var counts = seeder.Applications
            .GroupBy(a => a.RealEstateId)
            .Select(a => seeder.RealEstates.First(r => r.Id == a.Key).RealEstateType)
            .GroupBy(t => t)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(7, counts.Count);
        Assert.Contains(RealEstateType.Apartment, counts.Keys);
    }

    /// <summary>
    /// Вывод информации о клиентах, открывших заявки с минимальной стоимостью
    /// </summary>
    [Fact]
    public void Clients_WithMinimumAmountApplications()
    {
        var minAmount = seeder.Applications.Min(a => a.Amount);

        var clients = seeder.Applications
            .Where(a => a.Amount == minAmount)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        Assert.Single(clients);
        Assert.Equal(7, clients[0]);
    }

    /// <summary>
    /// Вывод сведений о всех клиентах, ищущих недвижимость заданного типа, упорядоченных по ФИО
    /// </summary>
    [Theory]
    [InlineData(RealEstateType.Apartment, 1)]
    [InlineData(RealEstateType.House, 2)]
    [InlineData(RealEstateType.Office, 4)]
    public void Clients_SearchingGivenRealEstateType(RealEstateType type, int expectedClientId)
    {
        var clients = seeder.Applications
            .Where(a => seeder.RealEstates.First(r => r.Id == a.RealEstateId).RealEstateType == type)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Contains(expectedClientId, clients);
    }
}
