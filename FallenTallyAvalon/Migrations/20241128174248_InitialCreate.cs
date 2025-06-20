using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FallenTally.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS 'Deaths' (
	                'DeathId' INTEGER NOT NULL,
	                'LocationId' INTEGER NOT NULL,
	                'StreamTime' INTEGER NOT NULL,
	                'TimeStamp' TEXT NOT NULL,
	                PRIMARY KEY ('DeathId')
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS 'GameStats' (
                    'GameId' INTEGER NOT NULL,
                    'GameName' TEXT NOT NULL,
                    'Prefix' TEXT NOT NULL,
                    PRIMARY KEY ('GameId')
                );
            "
            );

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS 'Locations' (
                    'LocationId' INTEGER NOT NULL,
                    'GameID' INTEGER NOT NULL,
                    'Name' TEXT NOT NULL,
                    PRIMARY KEY ('LocationId')
                );
            "
            );

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS 'Settings' (
                    'SettingsId' INTEGER NOT NULL,
                    'SettingsName' TEXT NOT NULL,
                    'SettingsValue' TEXT NOT NULL,
                    PRIMARY KEY ('SettingsId')
                );
                "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deaths");

            migrationBuilder.DropTable(
                name: "GameStats");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
