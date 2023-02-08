using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMuseoProgettoFinale.Migrations {
    /// <inheritdoc />
    public partial class QuantityMovedToProductNameAddedToPurchases : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.DropTable(
                name: "Stocks");

            _ = migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Purchases",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            _ = migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.DropColumn(
                name: "Name",
                table: "Purchases");

            _ = migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            _ = migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    _ = table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");
        }
    }
}
