using GatePassAPI.Enums;

namespace GatePassAPI.Finders.MotoFinder.DTOs;

public class MotoViewModel
{
	public Guid Id { get; set; }
	public Guid EventClassId { get; set; }
	public int MotoNumber { get; set; }
	public MotoType Type { get; set; }
	public MotoStatus Status { get; set; }
	public DateTime? StartTime { get; set; }
	public DateTime CreatedAt { get; set; }
}