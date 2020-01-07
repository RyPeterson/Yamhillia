using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class AddFarmKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Farms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_Key",
                table: "Farms",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Farms_Key",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Farms");
        }
    }
}
