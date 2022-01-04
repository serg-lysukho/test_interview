using Microsoft.EntityFrameworkCore.Migrations;

namespace InterviewApp.DAL.Migrations
{
    public partial class ChangeWatchlistItemFilmIdFieldSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists", 
                table: "WatchlistItems");

            migrationBuilder.AlterColumn<string>(
                name: "FilmId",
                table: "WatchlistItems",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                table: "WatchlistItems",
                columns: new[] { "FilmId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists",
                table: "WatchlistItems");

            migrationBuilder.AlterColumn<string>(
                name: "FilmId",
                table: "WatchlistItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                table: "WatchlistItems",
                columns: new[] { "FilmId", "UserId" });
        }
    }
}
