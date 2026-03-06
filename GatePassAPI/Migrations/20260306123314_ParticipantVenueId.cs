using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantVenueId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_last_name_first_name",
                table: "Participants");

            migrationBuilder.AddColumn<Guid>(
                name: "venue_id",
                table: "Participants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Participants_venue_id",
                table: "Participants",
                column: "venue_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Venues_venue_id",
                table: "Participants",
                column: "venue_id",
                principalTable: "Venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Venues_venue_id",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_venue_id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "venue_id",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_last_name_first_name",
                table: "Participants",
                columns: new[] { "last_name", "first_name" });
        }
    }
}
