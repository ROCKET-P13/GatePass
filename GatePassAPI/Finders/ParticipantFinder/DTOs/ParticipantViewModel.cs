namespace GatePassAPI.Finders.ParticipantFinder.DTOs;

public class ParticipantViewModel
{
	public required Guid Id { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required DateTime CreatedAt { get; set; }
}