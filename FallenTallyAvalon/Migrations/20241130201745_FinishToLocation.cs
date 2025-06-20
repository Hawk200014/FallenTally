using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FallenTally.Migrations
{
    /// <inheritdoc />
    public partial class FinishToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finish",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Locations");
        }
    }
}
