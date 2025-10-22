using RealEstateAgency.Domain.Data;
using RealEstateAgency.Domain.Enum;

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
            .Select(a => a.Counterparty)
            .Distinct()
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains(sellers, c => c.PassportNumber == "4010 123459");
        Assert.Contains(sellers, c => c.PassportNumber == "4010 123465");
        Assert.Equal(2, sellers.Count);
    }

    /// <summary>
    /// Вывод топ 5 клиентов по количеству заявок отдельно на покупку и продажу
    /// </summary>
    [Fact]
    public void Top5Clients_ByApplicationCount()
    {
        var topBuyers = seeder.Applications
            .Where(a => a.ApplicationType == ApplicationType.Purchase)
            .GroupBy(a => a.Counterparty)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .ToList();

        Assert.True(topBuyers.Count <= 5);

        var topSellers = seeder.Applications
            .Where(a => a.ApplicationType == ApplicationType.Sale)
            .GroupBy(a => a.Counterparty)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .ToList();

        Assert.True(topSellers.Count <= 5);
    }

    /// <summary>
    /// Вывод информации о количестве заявок по каждому типу недвижимости
    /// </summary>
    [Fact]
    public void ApplicationCount_ByRealEstateType()
    {
        var counts = seeder.Applications
            .GroupBy(a => a.RealEstate.RealEstateType)
            .ToDictionary(g => g.Key, g => g.Count());

        var expected = new Dictionary<RealEstateType, int>
        {
            { RealEstateType.Apartment, 2 },
            { RealEstateType.House, 2 },
            { RealEstateType.Room, 1 },
            { RealEstateType.Office, 2 },
            { RealEstateType.Retail, 1 },
            { RealEstateType.LandPlot, 1 },
            { RealEstateType.Garage, 1 }
        };

        foreach (var pair in expected)
        {
            Assert.True(counts.ContainsKey(pair.Key), $"Нет данных для {pair.Key}");
            Assert.Equal(pair.Value, counts[pair.Key]);
        }
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
            .Select(a => a.Counterparty)
            .Distinct()
            .ToList();

        Assert.NotEmpty(clients);

        Assert.Single(clients);
        Assert.Equal("Сергей", clients[0].FirstName);
        Assert.Equal("Васильев", clients[0].LastName);
        Assert.Equal("4010 123462", clients[0].PassportNumber);
    }

    /// <summary>
    /// Вывод сведений о всех клиентах, ищущих недвижимость заданного типа, упорядоченных по ФИО
    /// </summary>
    [Theory]
    [InlineData(RealEstateType.Apartment)]
    [InlineData(RealEstateType.House)]
    [InlineData(RealEstateType.Office)]
    public void Clients_SearchingGivenRealEstateType(RealEstateType type)
    {
        var clients = seeder.Applications
            .Where(a => a.RealEstate.RealEstateType == type)
            .Select(a => a.Counterparty)
            .Distinct()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();

        Assert.NotEmpty(clients);

        var expectedPassports = new Dictionary<RealEstateType, string[]>
        {
            { RealEstateType.Apartment, new[] { "4010 123456", "4010 123463" } },
            { RealEstateType.House, new[] { "4010 123457", "4010 123464" } },
            { RealEstateType.Office, new[] { "4010 123459", "4010 123465" } }
        };

        foreach (var passport in expectedPassports[type])
        {
            Assert.Contains(clients, c => c.PassportNumber == passport);
        }
    }
}
