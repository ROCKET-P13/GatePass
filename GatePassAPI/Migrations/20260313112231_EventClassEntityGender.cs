using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventClassEntityGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "capacity",
                table: "EventClasses");

            migrationBuilder.AddColumn<int>(
                name: "participant_capacity",
                table: "EventClasses",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "participant_capacity",
                table: "EventClasses");

            migrationBuilder.AddColumn<int>(
                name: "capacity",
                table: "EventClasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
