using System;
using Microsoft.EntityFrameworkCore.Migrations;
using YamhilliaNET.Utils;

namespace YamhilliaNET.Migrations
{
    public partial class AutogenUpdatedAndCreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string defaultValue = MigrationHelper.AutoUpdatingTimestampColumn(migrationBuilder);
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Farms",
                nullable: false,
                defaultValueSql: defaultValue,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Farms",
                nullable: false,
                defaultValueSql: defaultValue,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Animals",
                nullable: false,
                defaultValueSql: defaultValue,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Animals",
                nullable: false,
                defaultValueSql: defaultValue,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Unspecified).AddTicks(6390));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Farms",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Farms",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Animals",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Animals",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "current_timestamp");
        }
    }
}
