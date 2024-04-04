using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Authorization.Migrations
{
    /// <inheritdoc />
    public partial class update_defaultaddress_type_to_guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "default_address_id",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "default_address_id", "password_hash", "security_stamp" },
                values: new object[] { "93db7d2d-1ef0-431b-a714-4714599b1545", null, "AQAAAAIAAYagAAAAEJ+i+O2ViMmc0ZLHxgCMCzaXvfNJjQZhEh05aD+dcRTs2i+A5ZuS95hzFf4mT/MmQA==", "6a9f5afe-feb4-491c-b9c5-82181380e754" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "default_address_id", "password_hash", "security_stamp" },
                values: new object[] { "2c95bb82-37ca-4f43-ac41-2028aa81caa5", null, "AQAAAAIAAYagAAAAEKuhqQ8pcGbxQTKf6HAYYTBOmRMo0ZVeBMd+/lF26rPCH3VPmdRLyCLfW4eKU8U6tw==", "a9351aef-4b8b-4363-babb-2b7a8a8e1c71" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "default_address_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

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
    }
}
