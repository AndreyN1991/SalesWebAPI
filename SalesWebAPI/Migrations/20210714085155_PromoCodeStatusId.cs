using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebAPI.Migrations
{
    public partial class PromoCodeStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_PromoCodeStatuses_StatusId",
                table: "PromoCodes");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "PromoCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_PromoCodeStatuses_StatusId",
                table: "PromoCodes",
                column: "StatusId",
                principalTable: "PromoCodeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_PromoCodeStatuses_StatusId",
                table: "PromoCodes");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "PromoCodes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_PromoCodeStatuses_StatusId",
                table: "PromoCodes",
                column: "StatusId",
                principalTable: "PromoCodeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
