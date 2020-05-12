using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhillaNET.Migrations.SqliteMigrations
{
    public partial class UserEntityUUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityUUID",
                table: "Users",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityUUID",
                table: "Users");
        }
    }
}
