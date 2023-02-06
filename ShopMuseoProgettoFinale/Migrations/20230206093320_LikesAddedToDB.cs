using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMuseoProgettoFinale.Migrations {
    /// <inheritdoc />
    public partial class LikesAddedToDB : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            _ = migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    _ = table.PrimaryKey("PK_Like", x => new { x.UserId, x.ProductId });
                    _ = table.ForeignKey(
                        name: "FK_Like_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_Like_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Like_ProductId",
                table: "Like",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.DropTable(
                name: "Like");

            _ = migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
