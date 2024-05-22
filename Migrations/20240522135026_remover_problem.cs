using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraAlerta.Migrations
{
    /// <inheritdoc />
    public partial class remover_problem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problem_Address_Problem_pa_problempro_id",
                table: "Problem_Address");

            migrationBuilder.DropIndex(
                name: "IX_Problem_Address_pa_problempro_id",
                table: "Problem_Address");

            migrationBuilder.DropColumn(
                name: "pa_problempro_id",
                table: "Problem_Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pa_problempro_id",
                table: "Problem_Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Problem_Address_pa_problempro_id",
                table: "Problem_Address",
                column: "pa_problempro_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_Address_Problem_pa_problempro_id",
                table: "Problem_Address",
                column: "pa_problempro_id",
                principalTable: "Problem",
                principalColumn: "pro_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
