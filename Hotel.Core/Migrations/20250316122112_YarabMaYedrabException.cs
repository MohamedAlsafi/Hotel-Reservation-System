using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class YarabMaYedrabException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerDataId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerData", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 21, 11, 786, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 21, 11, 786, DateTimeKind.Local).AddTicks(3872));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 21, 11, 786, DateTimeKind.Local).AddTicks(3876));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 21, 11, 786, DateTimeKind.Local).AddTicks(3880));

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerDataId",
                table: "Reservation",
                column: "CustomerDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_CustomerData_CustomerDataId",
                table: "Reservation",
                column: "CustomerDataId",
                principalTable: "CustomerData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_CustomerData_CustomerDataId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "CustomerData");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CustomerDataId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CustomerDataId",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7827));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7882));
        }
    }
}
