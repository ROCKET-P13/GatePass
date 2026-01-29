namespace GatePassAPI.Factories.EventFactory.DTOs;

public class EventFactoryDTO
{
	public required Guid VenueId { get; set; }
	public required string Name { get; set; }
	public required DateTime Date { get; set; }
	public required string Status { get; set; }
	public int ParticipantCapacity { get; set; } = 0;
}