using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventClassRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Venues_venue_id",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_EventClasses_event_id",
                table: "EventClasses");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_event_number",
                table: "EventRegistrations",
                columns: new[] { "event_id", "event_number" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations",
                column: "event_class_id",
                principalTable: "EventClasses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_venue_id",
                table: "Events",
                column: "venue_id",
                principalTable: "Venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_venue_id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_event_id_event_number",
                table: "EventRegistrations");

            migrationBuilder.CreateIndex(
                name: "IX_EventClasses_event_id",
                table: "EventClasses",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations",
                column: "event_class_id",
                principalTable: "EventClasses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Venues_venue_id",
                table: "Participants",
                column: "venue_id",
                principalTable: "Venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
