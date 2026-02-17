using GatePassAPI.Enums;

namespace GatePassAPI.Controllers.DTOs;

public class UpdateEventRequest
{
	public string? Name { get; set; }
	public DateTimeOffset? StartDateTime { get; set; }
	public EventStatus? Status { get; set; }
	public int? ParticipantCapacity { get; set; }
}