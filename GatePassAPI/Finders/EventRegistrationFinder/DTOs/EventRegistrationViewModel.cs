namespace GatePassAPI.Finders.EventRegistrationFinder.DTOs;

public class EventRegistrationViewModel
{
	public Guid Id { get; set; }
	public Guid ParticipantId { get; set; }
	public required string ParticipantFirstName { get; set; }
	public required string ParticipantLastName { get; set; }
	public string? Class { get; set; }
	public int? EventNumber { get; set; }
	public required bool CheckedIn { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? CheckedInAt { get; set; }
}