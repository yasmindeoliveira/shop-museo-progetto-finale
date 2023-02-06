using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable
#nullable disable

namespace ShopMuseoProgettoFinale.Migrations
{
    /// <inheritdoc />
    public partial class ProductIdHotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_PurchasedProductId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Resupplies_Products_PurchasedProductId",
                table: "Resupplies");

            migrationBuilder.RenameColumn(
                name: "PurchasedProductId",
                table: "Resupplies",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Resupplies_PurchasedProductId",
                table: "Resupplies",
                newName: "IX_Resupplies_ProductId");

            migrationBuilder.RenameColumn(
                name: "PurchasedProductId",
                table: "Purchases",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PurchasedProductId",
                table: "Purchases",
                newName: "IX_Purchases_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resupplies_Products_ProductId",
                table: "Resupplies",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Resupplies_Products_ProductId",
                table: "Resupplies");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Resupplies",
                newName: "PurchasedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Resupplies_ProductId",
                table: "Resupplies",
                newName: "IX_Resupplies_PurchasedProductId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Purchases",
                newName: "PurchasedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                newName: "IX_Purchases_PurchasedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_PurchasedProductId",
                table: "Purchases",
                column: "PurchasedProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resupplies_Products_PurchasedProductId",
                table: "Resupplies",
                column: "PurchasedProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
