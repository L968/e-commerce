using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class variantfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_product_variation_option_variant_option_id",
                table: "product_variation_option",
                column: "variant_option_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variation_option_variant_options_variant_option_id",
                table: "product_variation_option",
                column: "variant_option_id",
                principalTable: "variant_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_variation_option_variant_options_variant_option_id",
                table: "product_variation_option");

            migrationBuilder.DropIndex(
                name: "ix_product_variation_option_variant_option_id",
                table: "product_variation_option");
        }
    }
}
