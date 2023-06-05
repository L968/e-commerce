using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    countryid = table.Column<int>(name: "country_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_country_country_id",
                        column: x => x.countryid,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sku = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    length = table.Column<float>(type: "float", nullable: false),
                    width = table.Column<float>(type: "float", nullable: false),
                    height = table.Column<float>(type: "float", nullable: false),
                    weight = table.Column<float>(type: "float", nullable: false),
                    productcategoryid = table.Column<int>(name: "product_category_id", type: "int", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_product_categories_product_category_id",
                        column: x => x.productcategoryid,
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stateid = table.Column<int>(name: "state_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_states_state_id",
                        column: x => x.stateid,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_image",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    imagepath = table.Column<string>(name: "image_path", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_image_product_product_id",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_inventory", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_inventory_product_product_id",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false),
                    recipientfullname = table.Column<string>(name: "recipient_full_name", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    recipientphonenumber = table.Column<string>(name: "recipient_phone_number", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    postalcode = table.Column<string>(name: "postal_code", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    streetname = table.Column<string>(name: "street_name", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    buildingnumber = table.Column<string>(name: "building_number", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    complement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    neighborhood = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cityid = table.Column<int>(name: "city_id", type: "int", nullable: false),
                    additionalinformation = table.Column<string>(name: "additional_information", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_address_cities_city_id",
                        column: x => x.cityid,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "id", "code", "name" },
                values: new object[] { 1, "BR", "Brazil" });

            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "code", "country_id", "name" },
                values: new object[,]
                {
                    { 1, "AC", 1, "Acre" },
                    { 2, "AL", 1, "Alagoas" },
                    { 3, "AP", 1, "Amapá" },
                    { 4, "AM", 1, "Amazonas" },
                    { 5, "BA", 1, "Bahia" },
                    { 6, "CE", 1, "Ceará" },
                    { 7, "ES", 1, "Espírito Santo" },
                    { 8, "GO", 1, "Goiás" },
                    { 9, "MA", 1, "Maranhão" },
                    { 10, "MT", 1, "Mato Grosso" },
                    { 11, "MS", 1, "Mato Grosso do Sul " },
                    { 12, "MG", 1, "Minas Gerais" },
                    { 13, "PA", 1, "Pará" },
                    { 14, "PB", 1, "Paraíba" },
                    { 15, "PR", 1, "Paraná" },
                    { 16, "PE", 1, "Pernambuco" },
                    { 17, "PI", 1, "Piauí" },
                    { 18, "RJ", 1, "Rio de Janeiro" },
                    { 19, "RN", 1, "Rio Grande do Norte" },
                    { 20, "RS", 1, "Rio Grande do Sul " },
                    { 21, "RO", 1, "Rondônia" },
                    { 22, "RR", 1, "Roraima" },
                    { 23, "SC", 1, "Santa Catarina " },
                    { 24, "SP", 1, "São Paulo" },
                    { 25, "SE", 1, "Sergipe" },
                    { 26, "TO", 1, "Tocantins" },
                    { 27, "DF", 1, "Distrito Federal " }
                });

            migrationBuilder.CreateIndex(
                name: "ix_address_city_id",
                table: "address",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_state_id",
                table: "city",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_category_id",
                table: "product",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_image_product_id",
                table: "product_image",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_inventory_product_id",
                table: "product_inventory",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_state_country_id",
                table: "state",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "product_image");

            migrationBuilder.DropTable(
                name: "product_inventory");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
