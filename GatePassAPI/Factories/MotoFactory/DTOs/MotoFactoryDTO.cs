namespace GatePassAPI.Factories.MotoFactory.DTOs;

public class MotoFactoryDTO
{
	public required Guid EventId { get; set; }
	public required Guid EventClassId { get; set; }
	public int? GateSize { get; set; } = null;
}