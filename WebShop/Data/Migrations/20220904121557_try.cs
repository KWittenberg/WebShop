using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class @try : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Product_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Product_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
