using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseToFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 17, 36, 51, 215, DateTimeKind.Local).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 17, 36, 51, 215, DateTimeKind.Local).AddTicks(6854));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 17, 36, 51, 215, DateTimeKind.Local).AddTicks(6856));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 17, 36, 51, 215, DateTimeKind.Local).AddTicks(6858));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "Feedbacks");

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 14, 41, 33, 403, DateTimeKind.Local).AddTicks(3862));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 14, 41, 33, 403, DateTimeKind.Local).AddTicks(3951));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 14, 41, 33, 403, DateTimeKind.Local).AddTicks(3955));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 14, 41, 33, 403, DateTimeKind.Local).AddTicks(3959));
        }
    }
}
