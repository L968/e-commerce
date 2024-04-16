using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Order.API.Migrations
{
    /// <inheritdoc />
    public partial class updating_order_payment_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "order");

            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "discount",
                table: "order",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "order",
                type: "decimal(65,2)",
                precision: 65,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
