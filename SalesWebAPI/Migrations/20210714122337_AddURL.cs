using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebAPI.Migrations
{
    public partial class AddURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "PromoCodes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "PromoCodes");
        }
    }
}
