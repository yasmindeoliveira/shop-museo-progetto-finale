using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMuseoProgettoFinale.Migrations {
    /// <inheritdoc />
    public partial class AddedValidation : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Resupplies",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            _ = migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Resupplies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);
        }
    }
}
