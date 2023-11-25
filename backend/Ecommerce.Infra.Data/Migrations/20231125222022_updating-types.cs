using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatingtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_products_combination_product_combination_id",
                table: "cart_item");

            migrationBuilder.DropTable(
                name: "product_variant_option");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "product_variation",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "product_variation_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_variation_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    variant_option_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variation_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_variation_option_product_variation_product_variation",
                        column: x => x.product_variation_id,
                        principalTable: "product_variation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_variant_option_variant_id",
                table: "variant_option",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variation_product_id",
                table: "product_variation",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variation_option_product_variation_id",
                table: "product_variation_option",
                column: "product_variation_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_product_combinations_product_combination_id",
                table: "cart_item",
                column: "product_combination_id",
                principalTable: "product_combination",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_variation_product_product_id",
                table: "product_variation",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_variant_option_variant_variant_id",
                table: "variant_option",
                column: "variant_id",
                principalTable: "variant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_product_combinations_product_combination_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "fk_product_variation_product_product_id",
                table: "product_variation");

            migrationBuilder.DropForeignKey(
                name: "fk_variant_option_variant_variant_id",
                table: "variant_option");

            migrationBuilder.DropTable(
                name: "product_variation_option");

            migrationBuilder.DropIndex(
                name: "ix_variant_option_variant_id",
                table: "variant_option");

            migrationBuilder.DropIndex(
                name: "ix_product_variation_product_id",
                table: "product_variation");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "product_variation",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "product_variant_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_variation_id = table.Column<int>(type: "int", nullable: false),
                    variant_option_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variant_option", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_products_combination_product_combination_id",
                table: "cart_item",
                column: "product_combination_id",
                principalTable: "product_combination",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
