namespace GatePassAPI.Entities;

public class EventRegistration
{
	public required Guid Id { get; set; }
	public required Guid EventId { get; set; }
	public Event Event { get; set; } = null!;
	public Guid ParticipantId { get; set; }
	public Participant Participant { get; set; } = null!;
	public string? Class { get; set; }
	public int? EventNumber { get; set; }
	public required bool CheckedIn { get; set; }
	public required DateTime CreatedAt { get; set; }
	public DateTime? CheckedInAt { get; set; }
}