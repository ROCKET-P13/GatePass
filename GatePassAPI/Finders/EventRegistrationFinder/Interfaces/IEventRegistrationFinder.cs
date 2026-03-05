using GatePassAPI.Entities;

namespace GatePassAPI.Finders.EventRegistrationFinder.Interfaces;

public interface IEventRegistrationFinder
{
	Task<List<EventRegistration>> GetByEventId(Guid eventId);
}