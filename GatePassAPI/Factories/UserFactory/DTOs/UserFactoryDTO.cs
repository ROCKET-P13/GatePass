namespace GatePassAPI.Factories.UserFactory.DTOs;

public class UserFactoryDTO
{
	public required string Auth0Id { get; set; }
	public Guid? VenueId { get; set; }
	public required string Email { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
}