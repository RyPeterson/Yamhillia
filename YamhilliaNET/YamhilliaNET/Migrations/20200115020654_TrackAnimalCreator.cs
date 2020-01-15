using Microsoft.EntityFrameworkCore.Migrations;

namespace YamhilliaNET.Migrations
{
    public partial class TrackAnimalCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CreatedById",
                table: "Animals",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_CreatedById",
                table: "Animals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_CreatedById",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CreatedById",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Animals");
        }
    }
}
