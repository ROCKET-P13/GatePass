using GatePassAPI.Enums;

namespace GatePassAPI.Finders.EventRegistrationFinder.DTOs;

public class ParticipantRegistrationViewModel
{
	public Guid Id { get; set; }
	public Guid EventId { get; set; }
	public DateTimeOffset EventDate { get; set; }
	public required string EventName { get; set; }
	public EventRegistrationType Type { get; set; }
	public int? EventNumber { get; set; }
	public DateTime CreatedAt { get; set; }
	public required bool CheckedIn { get; set; }
	public DateTime? CheckedInAt { get; set; }
}