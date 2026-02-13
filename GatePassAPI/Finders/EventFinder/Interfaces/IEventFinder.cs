using GatePassAPI.Entities;

namespace GatePassAPI.Finders.EventFinder.Interfaces;

public interface IEventFinder
{
	Task<List<Event>> GetAll();
	Task<List<Event>> GetByDateRange(Guid venueId, DateTime start, DateTime end);
	Task<Event?> GetById (Guid venueId, Guid eventId);
}