namespace GatePassAPI.Repositories.EventRegistrationRepository.DTOs;

public class EventRegistrationRepositoryGetByDTO
{
	public required Guid EventId { get; set; }
	public Guid ParticipantId { get; set; }
}