using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class ConstantDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Unspecified).AddTicks(6390), new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Unspecified).AddTicks(6390) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Utc).AddTicks(6393), new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Utc).AddTicks(6662) });
        }
    }
}
