using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstateAgency.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "counterparties",
                columns: new[] { "id", "first_name", "last_name", "passport_number", "patronymic", "phone_number" },
                values: new object[,]
                {
                    { 1, "Иван", "Иванов", "4010 123456", "Иванович", "89030000001" },
                    { 2, "Мария", "Петрова", "4010 123457", "Сергеевна", "89030000002" },
                    { 3, "Алексей", "Сидоров", "4010 123458", "Алексеевич", "89030000003" },
                    { 4, "Екатерина", "Кузнецова", "4010 123459", "Игоревна", "89030000004" },
                    { 5, "Дмитрий", "Новиков", "4010 123460", "Петрович", "89030000005" },
                    { 6, "Ольга", "Морозова", "4010 123461", "Сергеевна", "89030000006" },
                    { 7, "Сергей", "Васильев", "4010 123462", "Игоревич", "89030000007" },
                    { 8, "Анастасия", "Соколова", "4010 123463", "Александровна", "89030000008" },
                    { 9, "Никита", "Лебедев", "4010 123464", "Дмитриевич", "89030000009" },
                    { 10, "Татьяна", "Егорова", "4010 123465", "Викторовна", "89030000010" }
                });

            migrationBuilder.InsertData(
                table: "real_estates",
                columns: new[] { "id", "address", "cadastral_number", "ceiling_height", "floor_number", "has_encumbrances", "number_of_floors", "number_of_rooms", "real_estate_purpose", "real_estate_type", "total_area" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, д.1", "77:01:0001010:001", 2.7000000000000002, 2, false, 5, 2, 0, 0, 55.5 },
                    { 2, "ул. Пушкина, д.10", "77:01:0001020:002", 3.0, null, false, 2, 5, 0, 1, 120.0 },
                    { 3, "ул. Гагарина, д.5", "77:01:0001030:003", 2.5, 4, true, 9, 1, 0, 2, 18.0 },
                    { 4, "пр. Мира, д.20", "77:01:0001040:004", 3.2000000000000002, 5, false, 10, null, 1, 3, 250.0 },
                    { 5, "ул. Тверская, д.15", "77:01:0001050:005", 3.0, 1, false, 1, null, 1, 4, 80.0 },
                    { 6, "дер. Лесная, уч.5", "77:01:0001060:006", null, null, false, 0, null, 3, 5, 500.0 },
                    { 7, "ул. Парковая, д.7", "77:01:0001070:007", 2.5, 1, false, 1, null, 4, 6, 20.0 },
                    { 8, "ул. Чехова, д.8", "77:01:0001080:008", 2.7999999999999998, 6, true, 12, 3, 0, 0, 75.0 },
                    { 9, "ул. Лермонтова, д.12", "77:01:0001090:009", 2.8999999999999999, null, false, 1, 4, 0, 1, 90.0 },
                    { 10, "пр. Садовый, д.3", "77:01:0001100:010", 3.1000000000000001, 3, false, 5, null, 1, 3, 180.0 }
                });

            migrationBuilder.InsertData(
                table: "real_estate_applications",
                columns: new[] { "id", "amount", "application_type", "counterparty_id", "date_created", "real_estate_id" },
                values: new object[,]
                {
                    { 1, 5500000m, 0, 1, new DateOnly(2025, 11, 16), 1 },
                    { 2, 12000000m, 0, 2, new DateOnly(2025, 11, 6), 2 },
                    { 3, 1800000m, 0, 3, new DateOnly(2025, 11, 11), 3 },
                    { 4, 25000000m, 1, 4, new DateOnly(2025, 11, 21), 4 },
                    { 5, 8000000m, 1, 5, new DateOnly(2025, 11, 14), 5 },
                    { 6, 500000m, 0, 6, new DateOnly(2025, 11, 1), 6 },
                    { 7, 200000m, 1, 7, new DateOnly(2025, 11, 8), 7 },
                    { 8, 7500000m, 0, 8, new DateOnly(2025, 11, 24), 8 },
                    { 9, 9000000m, 0, 9, new DateOnly(2025, 11, 18), 9 },
                    { 10, 18000000m, 1, 10, new DateOnly(2025, 11, 25), 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "counterparties",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "real_estates",
                keyColumn: "id",
                keyValue: 10);
        }
    }
}
