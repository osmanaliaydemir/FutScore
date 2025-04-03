using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutScore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBCXE11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "HomeScore",
                table: "Matches",
                newName: "HomeTeamScore");

            migrationBuilder.RenameColumn(
                name: "AwayScore",
                table: "Matches",
                newName: "AwayTeamScore");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Founded",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Teams",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stadium",
                table: "Teams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Seasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Matches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StadiumId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Matches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StadiumId",
                table: "Matches",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Stadiums_StadiumId",
                table: "Matches",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Stadiums_StadiumId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Matches_StadiumId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Founded",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Stadium",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "HomeTeamScore",
                table: "Matches",
                newName: "HomeScore");

            migrationBuilder.RenameColumn(
                name: "AwayTeamScore",
                table: "Matches",
                newName: "AwayScore");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
