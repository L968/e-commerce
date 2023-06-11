using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_cities_city_id",
                table: "address");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropIndex(
                name: "ix_address_city_id",
                table: "address");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "address");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "address");

            migrationBuilder.DropColumn(
                name: "country",
                table: "address");

            migrationBuilder.DropColumn(
                name: "state",
                table: "address");

            migrationBuilder.AddColumn<int>(
                name: "city_id",
                table: "address",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
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
                    state_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_states_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
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
                name: "ix_state_country_id",
                table: "state",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "fk_address_cities_city_id",
                table: "address",
                column: "city_id",
                principalTable: "city",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
