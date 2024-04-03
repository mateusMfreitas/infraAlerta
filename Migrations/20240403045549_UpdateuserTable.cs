using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraAlerta.Migrations
{
    /// <inheritdoc />
    public partial class UpdateuserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_User_user_id",
                table: "User_Address");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "User_Address",
                newName: "ua_useruser_id");

            migrationBuilder.RenameIndex(
                name: "IX_User_Address_user_id",
                table: "User_Address",
                newName: "IX_User_Address_ua_useruser_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_User_ua_useruser_id",
                table: "User_Address",
                column: "ua_useruser_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_User_ua_useruser_id",
                table: "User_Address");

            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "login",
                table: "User");

            migrationBuilder.DropColumn(
                name: "password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ua_useruser_id",
                table: "User_Address",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_User_Address_ua_useruser_id",
                table: "User_Address",
                newName: "IX_User_Address_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_User_user_id",
                table: "User_Address",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
