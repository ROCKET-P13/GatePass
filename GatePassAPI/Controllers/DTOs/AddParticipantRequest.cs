namespace GatePassAPI.Controllers.DTOs;

public class AddParticipantRequest
{
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
}