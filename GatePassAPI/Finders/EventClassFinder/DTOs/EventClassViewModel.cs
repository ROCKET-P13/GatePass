using GatePassAPI.Enums;

namespace GatePassAPI.Finders.EventClassFinder.DTOs;

public class EventClassViewModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public int? MinimumAge { get; set; }
	public int? MaximumAge { get; set; }
	public string? SkillLevel { get; set; }
	public Gender Gender { get; set; }
	public int? ParticipantCapacity { get; set; }
	public DateTime? StartTime { get; set; }
}