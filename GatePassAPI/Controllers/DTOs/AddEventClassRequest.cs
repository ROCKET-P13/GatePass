namespace GatePassAPI.Controllers.DTOs;

public class AddEventClassRequest
{
	public required string Name { get; set; }
	public required Guid EventId { get; set; }
	public int? MininumAge { get; set; }
	public int? MaximumAge { get; set; }
	public string? SkillLevel { get; set; }

}