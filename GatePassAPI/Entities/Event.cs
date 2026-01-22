namespace GatePassAPI.Entities;

public class Event
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string Name { get; set; }
	public required string Date { get; set; }
	public string? ParticipantCapacity { get; set; }
}