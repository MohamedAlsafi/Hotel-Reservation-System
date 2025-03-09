using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class InitialCreateHotele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customer_CustomerId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_CustomerId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 30, 47, 623, DateTimeKind.Local).AddTicks(7256));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 30, 47, 623, DateTimeKind.Local).AddTicks(7327));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 30, 47, 623, DateTimeKind.Local).AddTicks(7329));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 30, 47, 623, DateTimeKind.Local).AddTicks(7331));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedbacks",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Customer_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customer_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 20, 28, 463, DateTimeKind.Local).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 20, 28, 463, DateTimeKind.Local).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 20, 28, 463, DateTimeKind.Local).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 9, 22, 20, 28, 463, DateTimeKind.Local).AddTicks(7752));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerId1",
                table: "Feedbacks",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Customer_CustomerId1",
                table: "Feedbacks",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
