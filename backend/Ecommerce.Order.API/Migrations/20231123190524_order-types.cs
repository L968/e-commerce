using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Order.API.Migrations
{
    /// <inheritdoc />
    public partial class ordertypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "product_unit_price",
                table: "order_item",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "product_discount",
                table: "order_item",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "order",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "shipping_cost",
                table: "order",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount",
                table: "order",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "product_unit_price",
                table: "order_item",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "product_discount",
                table: "order_item",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "order",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "shipping_cost",
                table: "order",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount",
                table: "order",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)",
                oldPrecision: 65,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
