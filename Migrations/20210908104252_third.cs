using Microsoft.EntityFrameworkCore.Migrations;

namespace E-Commerce_Backend.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_categoryid",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "categoryid",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_categoryid",
                table: "Product",
                column: "categoryid",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_categoryid",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "categoryid",
                table: "Product",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_categoryid",
                table: "Product",
                column: "categoryid",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
