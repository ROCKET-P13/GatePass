namespace GatePassAPI.Controllers.DTOs;

public class RegisterParticipantRequest
{
	public required Guid ParticipantId { get; set; }
	public string Class { get; set; } = string.Empty;
	public int EventNumber { get; set; }
	public bool CheckedIn { get; set; } = false;
}