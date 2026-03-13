using GatePassAPI.Enums;

namespace GatePassAPI.Controllers.DTOs;

public class AddEventClassRequest
{
	public required string Name { get; set; }
	public Gender Gender { get; set; }
	public string? SkillLevel { get; set; }
	public int? MaximumAge { get; set; }
	public int? MininumAge { get; set; }
	public int? ParticipantCapacity { get; set; }

}