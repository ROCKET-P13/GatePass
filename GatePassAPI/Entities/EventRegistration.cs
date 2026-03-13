using GatePassAPI.Enums;

namespace GatePassAPI.Entities;

public class EventRegistration
{
	public required Guid Id { get; set; }
	public required Guid EventId { get; set; }
	public required Guid ParticipantId { get; set; }
	public Participant Participant { get; set; } = null!;
	public required EventRegistrationType Type { get; set; }
	public int? EventNumber { get; set; }
	public required bool CheckedIn { get; set; }
	public required DateTime CreatedAt { get; set; }
	public DateTime? CheckedInAt { get; set; }
	public Guid? EventClassId { get; set; }
	
	public void CheckIn()
	{
		if (CheckedIn)
		{
			return;
		}

		CheckedIn = true;
		CheckedInAt = DateTime.UtcNow;
	}
}