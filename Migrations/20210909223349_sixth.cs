using Microsoft.EntityFrameworkCore.Migrations;

namespace E-Commerce_Backend.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_parentid",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_parentid",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "parentid",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_parentid",
                table: "Category",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_parentid",
                table: "Category",
                column: "parentid",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
