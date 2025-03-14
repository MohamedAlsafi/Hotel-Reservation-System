using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatesOnHotelStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "HotelStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "HotelStaff");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "HotelStaff");
        }
    }
}
