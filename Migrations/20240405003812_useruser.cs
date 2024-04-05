using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraAlerta.Migrations
{
    /// <inheritdoc />
    public partial class useruser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_User_ua_useruser_id",
                table: "User_Address");

            migrationBuilder.DropIndex(
                name: "IX_User_Address_ua_useruser_id",
                table: "User_Address");

            migrationBuilder.RenameColumn(
                name: "ua_useruser_id",
                table: "User_Address",
                newName: "ua_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ua_user_id",
                table: "User_Address",
                newName: "ua_useruser_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Address_ua_useruser_id",
                table: "User_Address",
                column: "ua_useruser_id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_User_ua_useruser_id",
                table: "User_Address",
                column: "ua_useruser_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
