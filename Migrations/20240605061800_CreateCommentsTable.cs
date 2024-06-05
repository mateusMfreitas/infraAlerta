using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraAlerta.Migrations
{
    public partial class CreateCommentsTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Comments",
            columns: table => new
            {
                comments_id = table.Column<int>(nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                pro_id = table.Column<int>(nullable: false),
                user_id = table.Column<int>(nullable: false),
                comments_text = table.Column<string>(nullable: false),
                created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.comments_id);
                table.ForeignKey(
                    name: "FK_Comments_Problem_pro_id",
                    column: x => x.pro_id,
                    principalTable: "Problem",
                    principalColumn: "pro_id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Comments_User_user_id",
                    column: x => x.user_id,
                    principalTable: "User",
                    principalColumn: "user_id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Comments");
    }
}
}
