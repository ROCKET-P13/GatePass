namespace GatePassAPI.Entities;

public class EventRegistration
{
	public Guid Id { get; set; }
	public Guid EventId { get; set; }
	public required Event Event { get; set; }
	public Guid ParticipantId { get; set; }
	public required Participant Participant { get; set; }
	public string? Class { get; set; }
	public int? EventNumber { get; set; }
	public bool CheckedIn { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? CheckedInAt { get; set; }
}