using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraAlerta.Migrations
{
    /// <inheritdoc />
    public partial class status_chamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pro_admin",
                table: "Problem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "pro_status",
                table: "Problem",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pro_admin",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "pro_status",
                table: "Problem");
        }
    }
}
