using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeedbackCustomerIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedback");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Feedback",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedback",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Feedback");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Feedback",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedback",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
