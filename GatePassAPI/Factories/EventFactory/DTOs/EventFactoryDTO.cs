namespace GatePassAPI.Factories.EventFactory.DTOs;

public class EventFactoryDTO
{
	public required Guid VenueId { get; set; }
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }

	public required string Status { get; set; }
	public int ParticipantCapacity { get; set; }
}