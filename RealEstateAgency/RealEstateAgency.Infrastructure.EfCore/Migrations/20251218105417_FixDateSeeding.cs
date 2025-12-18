using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class FixDateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 1,
                column: "date_created",
                value: new DateOnly(2025, 12, 8));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 2,
                column: "date_created",
                value: new DateOnly(2025, 11, 28));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 3,
                column: "date_created",
                value: new DateOnly(2025, 12, 3));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 4,
                column: "date_created",
                value: new DateOnly(2025, 12, 13));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 5,
                column: "date_created",
                value: new DateOnly(2025, 12, 6));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 6,
                column: "date_created",
                value: new DateOnly(2025, 11, 23));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 7,
                column: "date_created",
                value: new DateOnly(2025, 11, 30));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 8,
                column: "date_created",
                value: new DateOnly(2025, 12, 16));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 9,
                column: "date_created",
                value: new DateOnly(2025, 12, 10));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 10,
                column: "date_created",
                value: new DateOnly(2025, 12, 17));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 1,
                column: "date_created",
                value: new DateOnly(2025, 11, 16));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 2,
                column: "date_created",
                value: new DateOnly(2025, 11, 6));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 3,
                column: "date_created",
                value: new DateOnly(2025, 11, 11));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 4,
                column: "date_created",
                value: new DateOnly(2025, 11, 21));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 5,
                column: "date_created",
                value: new DateOnly(2025, 11, 14));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 6,
                column: "date_created",
                value: new DateOnly(2025, 11, 1));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 7,
                column: "date_created",
                value: new DateOnly(2025, 11, 8));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 8,
                column: "date_created",
                value: new DateOnly(2025, 11, 24));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 9,
                column: "date_created",
                value: new DateOnly(2025, 11, 18));

            migrationBuilder.UpdateData(
                table: "real_estate_applications",
                keyColumn: "id",
                keyValue: 10,
                column: "date_created",
                value: new DateOnly(2025, 11, 25));
        }
    }
}
