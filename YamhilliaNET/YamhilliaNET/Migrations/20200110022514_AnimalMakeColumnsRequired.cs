using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class AnimalMakeColumnsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Animals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
