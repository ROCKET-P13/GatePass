namespace GatePassAPI.Factories.EventRegistrationFactory.DTOs;

public class EventRegistrationFactoryCreateDTO
{
	public required Guid EventId { get; set; }
	public required Guid ParticipantId { get; set; }
	public int? EventNumber { get; set; }
	public string? Class { get; set; }
	public bool CheckedIn { get; set; } = false;
}