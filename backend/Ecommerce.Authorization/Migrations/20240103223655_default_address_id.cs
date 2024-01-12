using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Authorization.Migrations
{
    /// <inheritdoc />
    public partial class default_address_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "default_address_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "default_address_id", "password_hash", "security_stamp" },
                values: new object[] { "78359683-4652-4400-817b-7fed9658e314", null, "AQAAAAIAAYagAAAAEFJv7tKNvw620J5I3LRAA1kV+BDiSpWW7iB4dhmOCYqI+asl3Bu5DLXOOIDFFEgh7A==", "039fbce7-bf6b-4de9-80c4-6225490434b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "default_address_id", "password_hash", "security_stamp" },
                values: new object[] { "a88e746a-029a-41e2-a3aa-dd61fd26f638", null, "AQAAAAIAAYagAAAAEKFejq9hMvAMp+04RPeitLgdCC2AsTfymmH7PNcDhXxCkm756uDkCsqQOJ79PXMrqQ==", "8af50b42-73d8-429f-ad14-432848bd4c70" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "default_address_id",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "51627f21-a82d-4887-ac9a-a26900cf954d", "AQAAAAIAAYagAAAAEBwf6nwFBruL2gVChb1fAHfswwjI+Rnmsza1JRr/qDRmjoqOIrZ0VhcQyhe7/OrOrQ==", "38d2fad6-6a32-47aa-81cd-b71d1ce86d49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "cc116b7f-0f4e-4d74-bf6f-dd127f850e71", "AQAAAAIAAYagAAAAEFaa1eAmR/3Hmll2lAajnRS22FFrQLBcUc/oRsqy4ap9qrfSqDOgbUGVB/pUhWAXUA==", "2ddab000-7ea9-4898-a89e-cdb7c110bec2" });
        }
    }
}
