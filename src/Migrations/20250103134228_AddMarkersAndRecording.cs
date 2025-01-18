using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathCounterHotkey.Migrations
{
    /// <inheritdoc />
    public partial class AddMarkersAndRecording : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordingTime",
                table: "Deaths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    MarkerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    categorie = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StreamTime = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordingTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.MarkerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropColumn(
                name: "RecordingTime",
                table: "Deaths");
        }
    }
}
