using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class EstorYarabbbbbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_AspNetUsers_CustomerId1",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacility_Facility_FacilityId",
                table: "RoomFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacility_Room_RoomId1",
                table: "RoomFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImage_Room_RoomId",
                table: "RoomImage");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffer_Offer_OfferId",
                table: "RoomOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffer_Room_RoomId1",
                table: "RoomOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_Room_RoomId1",
                table: "RoomStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ReservationId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_CustomerId1",
                table: "Feedbacks",
                newName: "IX_Feedbacks_CustomerId1");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Room",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Feedbacks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(6951), null, false, "Wifi" },
                    { 2, new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7023), null, false, "TV" },
                    { 3, new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7028), null, false, "Mini Bar" },
                    { 4, new DateTime(2025, 3, 15, 0, 59, 29, 546, DateTimeKind.Local).AddTicks(7033), null, false, "air conditioning" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId1",
                table: "Feedbacks",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Reservation_ReservationId",
                table: "Feedbacks",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacility_Facility_FacilityId",
                table: "RoomFacility",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacility_Room_RoomId1",
                table: "RoomFacility",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImage_Room_RoomId",
                table: "RoomImage",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffer_Offer_OfferId",
                table: "RoomOffer",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffer_Room_RoomId1",
                table: "RoomOffer",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff",
                column: "StaffId",
                principalTable: "HotelStaff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_Room_RoomId1",
                table: "RoomStaff",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId1",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Reservation_ReservationId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacility_Facility_FacilityId",
                table: "RoomFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacility_Room_RoomId1",
                table: "RoomFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImage_Room_RoomId",
                table: "RoomImage");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffer_Offer_OfferId",
                table: "RoomOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOffer_Room_RoomId1",
                table: "RoomOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStaff_Room_RoomId1",
                table: "RoomStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DeleteData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ReservationId",
                table: "Feedback",
                newName: "IX_Feedback_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_CustomerId1",
                table: "Feedback",
                newName: "IX_Feedback_CustomerId1");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Room",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Room",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Feedback",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_AspNetUsers_CustomerId1",
                table: "Feedback",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacility_Facility_FacilityId",
                table: "RoomFacility",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacility_Room_RoomId1",
                table: "RoomFacility",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImage_Room_RoomId",
                table: "RoomImage",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffer_Offer_OfferId",
                table: "RoomOffer",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOffer_Room_RoomId1",
                table: "RoomOffer",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_HotelStaff_StaffId",
                table: "RoomStaff",
                column: "StaffId",
                principalTable: "HotelStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStaff_Room_RoomId1",
                table: "RoomStaff",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
