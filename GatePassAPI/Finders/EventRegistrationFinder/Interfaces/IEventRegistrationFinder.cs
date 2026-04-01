using GatePassAPI.Finders.EventRegistrationFinder.DTOs;

namespace GatePassAPI.Finders.EventRegistrationFinder.Interfaces;

public interface IEventRegistrationFinder
{
	Task<List<EventRegistrationViewModel>> GetByEventId(Guid eventId);
	Task<List<ParticipantRegistrationViewModel>> GetByParticipantId(Guid participantId);
	Task<List<ParticipantRegistrationViewModel>> GetCheckinsByEventId(Guid partcipantId);
}