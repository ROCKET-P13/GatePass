using GatePassAPI.Enums;

namespace GatePassAPI.Entities;

public class Event
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public required EventStatus Status { get; set; }
	public int? ParticipantCapacity { get; set; }
}