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
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    participant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    @class = table.Column<string>(name: "class", type: "text", nullable: true),
                    event_number = table.Column<int>(type: "integer", nullable: true),
                    checked_in = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    checked_in_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ParticipantId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => x.id);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Events_event_id",
                        column: x => x.event_id,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Participants_ParticipantId1",
                        column: x => x.ParticipantId1,
                        principalTable: "Participants",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "Participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_auth0_id",
                table: "Users",
                column: "auth0_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_venue_id",
                table: "Users",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_start_date_time",
                table: "Events",
                column: "start_date_time");

            migrationBuilder.CreateIndex(
                name: "IX_Events_venue_id",
                table: "Events",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id",
                table: "EventRegistrations",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_checked_in",
                table: "EventRegistrations",
                columns: new[] { "event_id", "checked_in" });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_id",
                table: "EventRegistrations",
                columns: new[] { "event_id", "id" });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_participant_id",
                table: "EventRegistrations",
                columns: new[] { "event_id", "participant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_participant_id",
                table: "EventRegistrations",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_ParticipantId1",
                table: "EventRegistrations",
                column: "ParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_last_name_first_name",
                table: "Participants",
                columns: new[] { "last_name", "first_name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Users_auth0_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_venue_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Events_start_date_time",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_venue_id",
                table: "Events");
        }
    }
}
