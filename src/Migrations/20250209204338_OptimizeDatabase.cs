using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathCounterHotkey.Migrations
{
    /// <inheritdoc />
    public partial class OptimizeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categorie",
                table: "Markers",
                newName: "Categorie");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_GameId",
                table: "Markers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_GameID",
                table: "Locations",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Deaths_LocationId",
                table: "Deaths",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deaths_Locations_LocationId",
                table: "Deaths",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_GameStats_GameID",
                table: "Locations",
                column: "GameID",
                principalTable: "GameStats",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_GameStats_GameId",
                table: "Markers",
                column: "GameId",
                principalTable: "GameStats",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deaths_Locations_LocationId",
                table: "Deaths");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_GameStats_GameID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_GameStats_GameId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_GameId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Locations_GameID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Deaths_LocationId",
                table: "Deaths");

            migrationBuilder.RenameColumn(
                name: "Categorie",
                table: "Markers",
                newName: "categorie");
        }
    }
}
