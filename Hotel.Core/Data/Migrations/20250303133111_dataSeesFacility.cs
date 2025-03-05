using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class dataSeesFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4682), null, false, "Wifi" },
                    { 2, new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4790), null, false, "TV" },
                    { 3, new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4794), null, false, "Mini Bar" },
                    { 4, new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4799), null, false, "air conditioning" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
