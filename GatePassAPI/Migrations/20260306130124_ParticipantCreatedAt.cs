using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_venue_id",
                table: "Participants");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Participants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Participants_venue_id_first_name_last_name",
                table: "Participants",
                columns: new[] { "venue_id", "first_name", "last_name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_venue_id_first_name_last_name",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_venue_id",
                table: "Participants",
                column: "venue_id");
        }
    }
}
