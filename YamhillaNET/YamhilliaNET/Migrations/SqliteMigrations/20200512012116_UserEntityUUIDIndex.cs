using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhillaNET.Migrations.SqliteMigrations
{
    public partial class UserEntityUUIDIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_EntityUUID",
                table: "Users",
                column: "EntityUUID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_EntityUUID",
                table: "Users");
        }
    }
}
