using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebAPI.Migrations
{
    public partial class ChangePromoCodeBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodeBooks_Books_BookId",
                table: "PromoCodeBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodeBooks_PromoCodes_PromoCodeId",
                table: "PromoCodeBooks");

            migrationBuilder.AlterColumn<int>(
                name: "PromoCodeId",
                table: "PromoCodeBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "PromoCodeBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodeBooks_Books_BookId",
                table: "PromoCodeBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodeBooks_PromoCodes_PromoCodeId",
                table: "PromoCodeBooks",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodeBooks_Books_BookId",
                table: "PromoCodeBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodeBooks_PromoCodes_PromoCodeId",
                table: "PromoCodeBooks");

            migrationBuilder.AlterColumn<int>(
                name: "PromoCodeId",
                table: "PromoCodeBooks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "PromoCodeBooks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodeBooks_Books_BookId",
                table: "PromoCodeBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodeBooks_PromoCodes_PromoCodeId",
                table: "PromoCodeBooks",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
