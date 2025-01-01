using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JudgeRegistrationRazor.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 14,
                column: "Value",
                value: "2025-02-29 23:59");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 15,
                column: "Value",
                value: "2024-09-28 00:00");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 21,
                column: "Value",
                value: "NoVA North");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 22,
                column: "Value",
                value: "9");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 25,
                column: "Value",
                value: "2025-01-25 23: 59");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 26,
                column: "Value",
                value: "2024-09-15 00: 00");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 14,
                column: "Value",
                value: "2024-02-29 23:59");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 15,
                column: "Value",
                value: "2023-11-28 00:00");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 21,
                column: "Value",
                value: "NoVA North and NoVA South");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 22,
                column: "Value",
                value: "9  & 12");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 25,
                column: "Value",
                value: "2024-1-25 23: 59");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 26,
                column: "Value",
                value: "2023-12-15 00: 00");
        }
    }
}
