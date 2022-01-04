using Microsoft.EntityFrameworkCore.Migrations;

namespace InterviewApp.DAL.Migrations
{
    public partial class RenameWatchlistToWatchlistItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Watchlists",
                newName: "WatchlistItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WatchlistItems",
                newName: "Watchlists");
        }
    }
}
