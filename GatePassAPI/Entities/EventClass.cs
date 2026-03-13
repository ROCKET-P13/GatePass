using GatePassAPI.Enums;

namespace GatePassAPI.Entities;

public class EventClass
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required Guid EventId { get; set; }
	public Event Event { get; set; } = null!;
	public int? MinimumAge { get; set; }
	public int? MaximumAge { get; set; }
	public string? SkillLevel { get; set; }
	public Gender Gender { get; set; }
	public int? ParticipantCapacity { get; set; }
	public DateTime? StartTime { get; set; }
	public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
}