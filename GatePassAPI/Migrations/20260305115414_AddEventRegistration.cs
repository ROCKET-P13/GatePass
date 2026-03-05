using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEventRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Participants_ParticipantId1",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_ParticipantId1",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "ParticipantId1",
                table: "EventRegistrations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParticipantId1",
                table: "EventRegistrations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_ParticipantId1",
                table: "EventRegistrations",
                column: "ParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Participants_ParticipantId1",
                table: "EventRegistrations",
                column: "ParticipantId1",
                principalTable: "Participants",
                principalColumn: "id");
        }
    }
}
