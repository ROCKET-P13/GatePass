using GatePassAPI.Enums;

namespace GatePassAPI.Factories.EventClassFactory.DTOs;

public class EventClassFactoryDTO
{
	public required Guid EventId { get; set; }
	public required string Name { get; set; }
	public int? MinimumAge { get; set; }
	public int? MaximumAge { get; set; }
	public string? SkillLevel { get; set; }
	public Gender Gender { get; set; }
	public int? ParticipantCapacity { get; set; }
}