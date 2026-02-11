namespace GatePassAPI.Entities;

public class Event
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string Name { get; set; }
	// public DateTime Date { get; set; }
	// public TimeSpan StartTime { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public required string Status { get; set; }
	public int? ParticipantCapacity { get; set; }
}