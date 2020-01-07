using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class SeedDefaultFarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "Key", "Name", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1L, "DEFAULT", "Default Farm", DateTime.UtcNow, DateTime.UtcNow });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
