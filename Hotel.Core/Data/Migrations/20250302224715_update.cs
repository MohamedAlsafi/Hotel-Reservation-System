using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomFacilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RoomFacilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RoomFacilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_RoomId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Feedbacks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFacilities",
                table: "RoomFacilities",
                columns: new[] { "RoomId", "FacilityId" });
        }
    }
}
