namespace GatePassAPI.Entities;

public class Participant
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required DateTime CreatedAt { get; set; }
	public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
}