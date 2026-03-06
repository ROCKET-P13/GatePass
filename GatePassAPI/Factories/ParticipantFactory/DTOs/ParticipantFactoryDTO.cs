namespace GatePassAPI.Factories.ParticipantFactory.DTOs;

public class ParticipantFactoryDTO
{
	public required Guid VenueId { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
}