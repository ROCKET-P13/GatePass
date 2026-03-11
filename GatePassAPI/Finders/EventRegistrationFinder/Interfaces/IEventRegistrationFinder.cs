using GatePassAPI.Finders.EventRegistrationFinder.DTOs;

namespace GatePassAPI.Finders.EventRegistrationFinder.Interfaces;

public interface IEventRegistrationFinder
{
	Task<List<EventRegistrationViewModel>> GetByEventId(Guid eventId);
}