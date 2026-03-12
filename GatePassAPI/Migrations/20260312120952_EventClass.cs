using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePassAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "class",
                table: "EventRegistrations");

            migrationBuilder.AddColumn<Guid>(
                name: "event_class_id",
                table: "EventRegistrations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "EventRegistrations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventClasses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    minimum_age = table.Column<int>(type: "integer", nullable: true),
                    maximum_age = table.Column<int>(type: "integer", nullable: true),
                    skill_level = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventClasses", x => x.id);
                    table.ForeignKey(
                        name: "FK_EventClasses_Events_event_id",
                        column: x => x.event_id,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_event_class_id",
                table: "EventRegistrations",
                column: "event_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventClasses_event_id",
                table: "EventClasses",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventClasses_event_id_name",
                table: "EventClasses",
                columns: new[] { "event_id", "name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations",
                column: "event_class_id",
                principalTable: "EventClasses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_EventClasses_event_class_id",
                table: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "EventClasses");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_event_class_id",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "event_class_id",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "type",
                table: "EventRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "class",
                table: "EventRegistrations",
                type: "text",
                nullable: true);
        }
    }
}
