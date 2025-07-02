using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FallenTally.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordingToMarkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordingSession",
                table: "Markers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreamSession",
                table: "Markers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordingSession",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "StreamSession",
                table: "Markers");
        }
    }
}
