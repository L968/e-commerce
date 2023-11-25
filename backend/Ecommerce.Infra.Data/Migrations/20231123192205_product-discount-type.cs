using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class productdiscounttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "maximum_discount_amount",
                table: "product_discount",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_value",
                table: "product_discount",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "maximum_discount_amount",
                table: "product_discount",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_value",
                table: "product_discount",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2);
        }
    }
}
