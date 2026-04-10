namespace GatePassAPI.Entities;

public class MotoEntry
{
	public Guid Id { get; set; }
	public Guid MotoId { get; set; }
	public Moto Moto { get; set; } = null!;
	public Guid EventRegistrationId { get; set; }
	public EventRegistration EventRegistration { get; set; } = null!;
	public int GatePick { get; set; }
	public int? FinishPosition { get; set; }
	public int? Points { get; set; }
	public DateTime CreatedAt { get; set; }
}