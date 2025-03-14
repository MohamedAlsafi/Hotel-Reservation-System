using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class EstorYarabbbA33333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaff_StaffId",
                table: "RoomStaff");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HotelStaff");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "HotelStaff",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "HotelStaff",
                newName: "SecurityStamp");

            migrationBuilder.AddColumn<string>(
                name: "StaffId1",
                table: "RoomStaff",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByStaffId",
                table: "Offer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "HotelStaff",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "HotelStaff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "HotelStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "HotelStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "HotelStaff",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "HotelStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5276));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaff_StaffId1",
                table: "RoomStaff",
                column: "StaffId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId1",
                table: "RoomStaff",
                column: "StaffId1",
                principalTable: "HotelStaff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId1",
                table: "RoomStaff");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaff_StaffId1",
                table: "RoomStaff");

            migrationBuilder.DropColumn(
                name: "StaffId1",
                table: "RoomStaff");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "HotelStaff");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "HotelStaff",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "HotelStaff",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByStaffId",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HotelStaff",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HotelStaff",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(6951));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7033));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaff_StaffId",
                table: "RoomStaff",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff",
                column: "StaffId",
                principalTable: "HotelStaff",
                principalColumn: "Id");
        }
    }
}
