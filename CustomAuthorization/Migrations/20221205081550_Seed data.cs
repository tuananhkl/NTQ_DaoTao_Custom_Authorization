using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorization.Migrations
{
    public partial class Seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "GroupName", "Status" },
                values: new object[] { 1, "Admin", false });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Action", "Controller" },
                values: new object[] { 1, "Index", "User" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "Status", "UserId" },
                values: new object[] { 1, 1, true, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Age", "DateOfBirth", "Email", "Gender", "GroupId", "Password", "Status", "UserName" },
                values: new object[] { 1, "Hanoi", 23, new DateTime(1999, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuananh@gmail.com", true, 1, "Tuananh123.", true, "tuananh" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
