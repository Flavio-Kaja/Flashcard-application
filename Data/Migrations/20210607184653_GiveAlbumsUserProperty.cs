using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashcardsApp.Data.Migrations
{
    public partial class GiveAlbumsUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "albums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userid",
                table: "albums");
        }
    }
}
