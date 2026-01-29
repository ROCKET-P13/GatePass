namespace GatePassAPI.Entities;

public class Event
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string Name { get; set; }
	public required DateTime Date { get; set; }
	public int? ParticipantCapacity { get; set; }
	public required string Status { get; set; }
}