using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class MotosAndMotoEntires : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_event_id_participant_id",
                table: "EventRegistrations");

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_number = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Motos_EventClasses_EventClassId",
                        column: x => x.EventClassId,
                        principalTable: "EventClasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotoEntries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    registration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gate_pick = table.Column<int>(type: "integer", nullable: false),
                    finish_position = table.Column<int>(type: "integer", nullable: true),
                    points = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_MotoEntries_EventRegistrations_registration_id",
                        column: x => x.registration_id,
                        principalTable: "EventRegistrations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotoEntries_Motos_moto_id",
                        column: x => x.moto_id,
                        principalTable: "Motos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_participant_id_event_class_id",
                table: "EventRegistrations",
                columns: new[] { "event_id", "participant_id", "event_class_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotoEntries_moto_id_gate_pick",
                table: "MotoEntries",
                columns: new[] { "moto_id", "gate_pick" });

            migrationBuilder.CreateIndex(
                name: "IX_MotoEntries_registration_id",
                table: "MotoEntries",
                column: "registration_id");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_EventClassId",
                table: "Motos",
                column: "EventClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_EventClassId_moto_number",
                table: "Motos",
                columns: new[] { "EventClassId", "moto_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotoEntries");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_event_id_participant_id_event_class_id",
                table: "EventRegistrations");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_id_participant_id",
                table: "EventRegistrations",
                columns: new[] { "event_id", "participant_id" },
                unique: true);
        }
    }
}
