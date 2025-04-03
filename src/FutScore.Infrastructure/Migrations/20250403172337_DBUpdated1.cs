using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutScore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamId1",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Teams_TeamId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_TeamId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_LeagueId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamId1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId_Name",
                table: "Teams",
                columns: new[] { "LeagueId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_Name_City",
                table: "Stadiums",
                columns: new[] { "Name", "City" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_LeagueId_SeasonName",
                table: "Seasons",
                columns: new[] { "LeagueId", "SeasonName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId_JerseyNumber",
                table: "Players",
                columns: new[] { "TeamId", "JerseyNumber" },
                unique: true,
                filter: "[JerseyNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeagueId_Name",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_Name_City",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_LeagueId_SeasonName",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId_JerseyNumber",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Stadiums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId1",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_TeamId",
                table: "Stadiums",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_LeagueId",
                table: "Seasons",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId1",
                table: "Matches",
                column: "TeamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamId",
                table: "Matches",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamId1",
                table: "Matches",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Teams_TeamId",
                table: "Stadiums",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
