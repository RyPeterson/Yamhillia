using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class AnimalCustomIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomIdentifier",
                table: "Animals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomIdentifier",
                table: "Animals");
        }
    }
}
