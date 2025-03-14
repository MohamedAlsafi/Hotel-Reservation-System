using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class TestForForiegnKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId",
                table: "RoomFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffers_Rooms_RoomId",
                table: "RoomOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaffs_Rooms_RoomId",
                table: "RoomStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomStaffs",
                table: "RoomStaffs");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaffs_RoomId",
                table: "RoomStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomOffers",
                table: "RoomOffers");

            migrationBuilder.DropIndex(
                name: "IX_RoomOffers_RoomId",
                table: "RoomOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomStaffs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "RoomStaffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomOffers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "RoomOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomStaffs",
                table: "RoomStaffs",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomOffers",
                table: "RoomOffers",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities",
                column: "RoomId");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 53, 6, 962, DateTimeKind.Local).AddTicks(4619));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 53, 6, 962, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 53, 6, 962, DateTimeKind.Local).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 53, 6, 962, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaffs_RoomId1",
                table: "RoomStaffs",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaffs_StaffId",
                table: "RoomStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomOffers_OfferId",
                table: "RoomOffers",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomOffers_RoomId1",
                table: "RoomOffers",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_RoomId1",
                table: "RoomFacilities",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId1",
                table: "RoomFacilities",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffers_Rooms_RoomId1",
                table: "RoomOffers",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaffs_Rooms_RoomId1",
                table: "RoomStaffs",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId1",
                table: "RoomFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffers_Rooms_RoomId1",
                table: "RoomOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaffs_Rooms_RoomId1",
                table: "RoomStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomStaffs",
                table: "RoomStaffs");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaffs_RoomId1",
                table: "RoomStaffs");

            migrationBuilder.DropIndex(
                name: "IX_RoomStaffs_StaffId",
                table: "RoomStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomOffers",
                table: "RoomOffers");

            migrationBuilder.DropIndex(
                name: "IX_RoomOffers_OfferId",
                table: "RoomOffers");

            migrationBuilder.DropIndex(
                name: "IX_RoomOffers_RoomId1",
                table: "RoomOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_RoomId1",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "RoomStaffs");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "RoomOffers");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "RoomFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomStaffs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomOffers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomStaffs",
                table: "RoomStaffs",
                columns: new[] { "StaffId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomOffers",
                table: "RoomOffers",
                columns: new[] { "OfferId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 48, 26, 367, DateTimeKind.Local).AddTicks(5545));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 48, 26, 367, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 48, 26, 367, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 48, 26, 367, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.CreateIndex(
                name: "IX_RoomStaffs_RoomId",
                table: "RoomStaffs",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomOffers_RoomId",
                table: "RoomOffers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Rooms_RoomId",
                table: "RoomFacilities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffers_Rooms_RoomId",
                table: "RoomOffers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaffs_Rooms_RoomId",
                table: "RoomStaffs",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
