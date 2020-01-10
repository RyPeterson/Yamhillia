using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class MakeUserFarmIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "FarmId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "FarmId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 7, 3, 48, 9, 644, DateTimeKind.Unspecified).AddTicks(6390));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
