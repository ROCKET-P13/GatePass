using GatePassAPI.Enums;

namespace GatePassAPI.Finders.EventFinder.DTOs;

public class EventViewModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public required EventStatus Status { get; set; }
	public int? ParticipantCapacity { get; set; }
}