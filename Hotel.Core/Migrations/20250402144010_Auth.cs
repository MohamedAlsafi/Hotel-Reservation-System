using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaffs_HotelStaffs_StaffId",
                table: "RoomStaffs");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaffs_StaffId",
                table: "RoomStaffs");

            migrationBuilder.AddColumn<int>(
                name: "hotelStaffId",
                table: "RoomStaffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoleFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roles = table.Column<int>(type: "int", nullable: false),
                    Features = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFeatures", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 2, 14, 40, 9, 672, DateTimeKind.Utc).AddTicks(3294));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 2, 14, 40, 9, 672, DateTimeKind.Utc).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 2, 14, 40, 9, 672, DateTimeKind.Utc).AddTicks(3297));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 2, 14, 40, 9, 672, DateTimeKind.Utc).AddTicks(3298));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaffs_hotelStaffId",
                table: "RoomStaffs",
                column: "hotelStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaffs_HotelStaffs_hotelStaffId",
                table: "RoomStaffs",
                column: "hotelStaffId",
                principalTable: "HotelStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaffs_HotelStaffs_hotelStaffId",
                table: "RoomStaffs");

            migrationBuilder.DropTable(
                name: "RoleFeatures");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaffs_hotelStaffId",
                table: "RoomStaffs");

            migrationBuilder.DropColumn(
                name: "hotelStaffId",
                table: "RoomStaffs");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 23, 19, 46, 19, 23, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 23, 19, 46, 19, 23, DateTimeKind.Utc).AddTicks(6205));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 23, 19, 46, 19, 23, DateTimeKind.Utc).AddTicks(6206));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 23, 19, 46, 19, 23, DateTimeKind.Utc).AddTicks(6207));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaffs_StaffId",
                table: "RoomStaffs",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaffs_HotelStaffs_StaffId",
                table: "RoomStaffs",
                column: "StaffId",
                principalTable: "HotelStaffs",
                principalColumn: "Id");
        }
    }
}
