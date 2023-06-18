using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class CreateTableNewProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductImage_ProductImageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductImageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImageId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductId",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ProductId",
                table: "Product",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ProductId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductImageId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductImageId",
                table: "Product",
                column: "ProductImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductImage_ProductImageId",
                table: "Product",
                column: "ProductImageId",
                principalTable: "ProductImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
