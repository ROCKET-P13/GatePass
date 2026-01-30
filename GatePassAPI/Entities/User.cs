namespace GatePassAPI.Entities;

public class User
{
	public required Guid Id { get; set; }
	public required string Auth0Id { get; set; }
	public Guid? VenueId { get; set; }
	public required string Email { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required DateTime CreatedAt { get; set; }
}