using RealEstateAgency.Domain.Enum;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Domain.Data;

/// <summary>
/// Фикстура с тестовыми данными для риэлторского агентства
/// </summary>
public class RealEstateSeeder
{
    /// <summary>
    /// Коллекция контрагентов
    /// </summary>
    public List<Counterparty> Counterparties { get; } = [];

    /// <summary>
    /// Коллекция объектов недвижимости
    /// </summary>
    public List<RealEstate> RealEstates { get; } = [];

    /// <summary>
    /// Коллекция заявок на недвижимость
    /// </summary>
    public List<RealEstateApplication> Applications { get; } = [];

    /// <summary>
    /// Конструктор, заполняющий фикстуру данными
    /// </summary>
    public RealEstateSeeder()
    {
        Counterparties.AddRange(
        [
            new Counterparty
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                PassportNumber = "4010 123456",
                PhoneNumber = "89030000001"
            },
            new Counterparty
            {
                Id = 2,
                FirstName = "Мария",
                LastName = "Петрова",
                Patronymic = "Сергеевна",
                PassportNumber = "4010 123457",
                PhoneNumber = "89030000002"
            },
            new Counterparty
            {
                Id = 3,
                FirstName = "Алексей",
                LastName = "Сидоров",
                Patronymic = "Алексеевич",
                PassportNumber = "4010 123458",
                PhoneNumber = "89030000003"
            },
            new Counterparty
            {
                Id = 4,
                FirstName = "Екатерина",
                LastName = "Кузнецова",
                Patronymic = "Игоревна",
                PassportNumber = "4010 123459",
                PhoneNumber = "89030000004"
            },
            new Counterparty
            {
                Id = 5,
                FirstName = "Дмитрий",
                LastName = "Новиков",
                Patronymic = "Петрович",
                PassportNumber = "4010 123460",
                PhoneNumber = "89030000005"
            },
            new Counterparty
            {
                Id = 6,
                FirstName = "Ольга",
                LastName = "Морозова",
                Patronymic = "Сергеевна",
                PassportNumber = "4010 123461",
                PhoneNumber = "89030000006"
            },
            new Counterparty
            {
                Id = 7,
                FirstName = "Сергей",
                LastName = "Васильев",
                Patronymic = "Игоревич",
                PassportNumber = "4010 123462",
                PhoneNumber = "89030000007"
            },
            new Counterparty
            {
                Id = 8,
                FirstName = "Анастасия",
                LastName = "Соколова",
                Patronymic = "Александровна",
                PassportNumber = "4010 123463",
                PhoneNumber = "89030000008"
            },
            new Counterparty
            {
                Id = 9,
                FirstName = "Никита",
                LastName = "Лебедев",
                Patronymic = "Дмитриевич",
                PassportNumber = "4010 123464",
                PhoneNumber = "89030000009"
            },
            new Counterparty
            {
                Id = 10,
                FirstName = "Татьяна",
                LastName = "Егорова",
                Patronymic = "Викторовна",
                PassportNumber = "4010 123465",
                PhoneNumber = "89030000010"
            }
        ]);

        RealEstates.AddRange(
        [
            new RealEstate
            {
                Id = 1,
                RealEstateType = RealEstateType.Apartment,
                RealEstatePurpose = RealEstatePurpose.Residential,
                CadastralNumber = "77:01:0001010:001",
                Address = "ул. Ленина, д.1",
                NumberOfFloors = 5,
                TotalArea = 55.5,
                NumberOfRooms = 2,
                CeilingHeight = 2.7,
                FloorNumber = 2,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 2,
                RealEstateType = RealEstateType.House,
                RealEstatePurpose = RealEstatePurpose.Residential,
                CadastralNumber = "77:01:0001020:002",
                Address = "ул. Пушкина, д.10",
                NumberOfFloors = 2,
                TotalArea = 120.0,
                NumberOfRooms = 5,
                CeilingHeight = 3.0,
                FloorNumber = null,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 3,
                RealEstateType = RealEstateType.Room,
                RealEstatePurpose = RealEstatePurpose.Residential,
                CadastralNumber = "77:01:0001030:003",
                Address = "ул. Гагарина, д.5",
                NumberOfFloors = 9,
                TotalArea = 18.0,
                NumberOfRooms = 1,
                CeilingHeight = 2.5,
                FloorNumber = 4,
                HasEncumbrances = true
            },
            new RealEstate
            {
                Id = 4,
                RealEstateType = RealEstateType.Office,
                RealEstatePurpose = RealEstatePurpose.Commercial,
                CadastralNumber = "77:01:0001040:004",
                Address = "пр. Мира, д.20",
                NumberOfFloors = 10,
                TotalArea = 250.0,
                NumberOfRooms = null,
                CeilingHeight = 3.2,
                FloorNumber = 5,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 5,
                RealEstateType = RealEstateType.Retail,
                RealEstatePurpose = RealEstatePurpose.Commercial,
                CadastralNumber = "77:01:0001050:005",
                Address = "ул. Тверская, д.15",
                NumberOfFloors = 1,
                TotalArea = 80.0,
                NumberOfRooms = null,
                CeilingHeight = 3.0,
                FloorNumber = 1,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 6,
                RealEstateType = RealEstateType.LandPlot,
                RealEstatePurpose = RealEstatePurpose.Agricultural,
                CadastralNumber = "77:01:0001060:006",
                Address = "дер. Лесная, уч.5",
                NumberOfFloors = 0,
                TotalArea = 500.0,
                NumberOfRooms = null,
                CeilingHeight = null,
                FloorNumber = null,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 7,
                RealEstateType = RealEstateType.Garage,
                RealEstatePurpose = RealEstatePurpose.Mixed,
                CadastralNumber = "77:01:0001070:007",
                Address = "ул. Парковая, д.7",
                NumberOfFloors = 1,
                TotalArea = 20.0,
                NumberOfRooms = null,
                CeilingHeight = 2.5,
                FloorNumber = 1,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 8,
                RealEstateType = RealEstateType.Apartment,
                RealEstatePurpose = RealEstatePurpose.Residential,
                CadastralNumber = "77:01:0001080:008",
                Address = "ул. Чехова, д.8",
                NumberOfFloors = 12,
                TotalArea = 75.0,
                NumberOfRooms = 3,
                CeilingHeight = 2.8,
                FloorNumber = 6,
                HasEncumbrances = true
            },
            new RealEstate
            {
                Id = 9,
                RealEstateType = RealEstateType.House,
                RealEstatePurpose = RealEstatePurpose.Residential,
                CadastralNumber = "77:01:0001090:009",
                Address = "ул. Лермонтова, д.12",
                NumberOfFloors = 1,
                TotalArea = 90.0,
                NumberOfRooms = 4,
                CeilingHeight = 2.9,
                FloorNumber = null,
                HasEncumbrances = false
            },
            new RealEstate
            {
                Id = 10,
                RealEstateType = RealEstateType.Office,
                RealEstatePurpose = RealEstatePurpose.Commercial,
                CadastralNumber = "77:01:0001100:010",
                Address = "пр. Садовый, д.3",
                NumberOfFloors = 5,
                TotalArea = 180.0,
                NumberOfRooms = null,
                CeilingHeight = 3.1,
                FloorNumber = 3,
                HasEncumbrances = false
            }
        ]);

        Applications.AddRange(
        [
            new RealEstateApplication
            {
                Id = 1,
                Counterparty = Counterparties[0],
                RealEstate = RealEstates[0],
                ApplicationType = ApplicationType.Purchase,
                Amount = 5500000
            },
            new RealEstateApplication
            {
                Id = 2,
                Counterparty = Counterparties[1],
                RealEstate = RealEstates[1],
                ApplicationType = ApplicationType.Purchase,
                Amount = 12000000
            },
            new RealEstateApplication
            {
                Id = 3,
                Counterparty = Counterparties[2],
                RealEstate = RealEstates[2],
                ApplicationType = ApplicationType.Purchase,
                Amount = 1800000
            },
            new RealEstateApplication
            {
                Id = 4,
                Counterparty = Counterparties[3],
                RealEstate = RealEstates[3],
                ApplicationType = ApplicationType.Sale,
                Amount = 25000000
            },
            new RealEstateApplication
            {
                Id = 5,
                Counterparty = Counterparties[4],
                RealEstate = RealEstates[4],
                ApplicationType = ApplicationType.Sale,
                Amount = 8000000
            },
            new RealEstateApplication
            {
                Id = 6,
                Counterparty = Counterparties[5],
                RealEstate = RealEstates[5],
                ApplicationType = ApplicationType.Purchase,
                Amount = 500000
            },
            new RealEstateApplication
            {
                Id = 7,
                Counterparty = Counterparties[6],
                RealEstate = RealEstates[6],
                ApplicationType = ApplicationType.Sale,
                Amount = 200000
            },
            new RealEstateApplication
            {
                Id = 8,
                Counterparty = Counterparties[7],
                RealEstate = RealEstates[7],
                ApplicationType = ApplicationType.Purchase,
                Amount = 7500000
            },
            new RealEstateApplication
            {
                Id = 9,
                Counterparty = Counterparties[8],
                RealEstate = RealEstates[8],
                ApplicationType = ApplicationType.Purchase,
                Amount = 9000000
            },
            new RealEstateApplication
            {
                Id = 10,
                Counterparty = Counterparties[9],
                RealEstate = RealEstates[9],
                ApplicationType = ApplicationType.Sale,
                Amount = 18000000
            }
        ]);
    }
}