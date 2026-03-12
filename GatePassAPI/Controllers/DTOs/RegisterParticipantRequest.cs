using GatePassAPI.Enums;

namespace GatePassAPI.Controllers.DTOs;

public class RegisterParticipantRequest
{
	public required Guid ParticipantId { get; set; }
	public Guid? EventClassId { get; set; }
	public int EventNumber { get; set; }
	public bool CheckedIn { get; set; } = false;
	public EventRegistrationType Type { get; set; } = EventRegistrationType.Spectator;
}