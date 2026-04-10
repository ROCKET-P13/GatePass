using GatePassAPI.Enums;

namespace GatePassAPI.Entities;

public class Moto
{
	public Guid Id { get; set; }
	public EventClass EventClass { get; set; } = null!;
	public Guid EventClassId { get; set; }
	public int MotoNumber { get; set; }
	public MotoType Type { get; set; }
	public MotoStatus Status { get; set; }
	public DateTime? StartTime { get; set; }
	public ICollection<MotoEntry> Entries { get; set; } = new List<MotoEntry>();
	public DateTime CreatedAt { get; set; }
}