using GatePassAPI.Enums;

namespace GatePassAPI.Controllers.DTOs;

public class AddEventRequest
{
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public EventStatus Status { get; set; } = EventStatus.Draft;
	public int? ParticipantCapacity { get; set; }
}