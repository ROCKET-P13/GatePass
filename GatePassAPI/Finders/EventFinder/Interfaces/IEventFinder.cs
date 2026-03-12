using GatePassAPI.Entities;
using GatePassAPI.Finders.EventFinder.DTOs;

namespace GatePassAPI.Finders.EventFinder.Interfaces;

public interface IEventFinder
{
	Task<List<EventViewModel>> GetAll();
	Task<List<EventViewModel>> GetByDateRange(Guid venueId, DateTime start, DateTime end);
	Task<Event?> GetById(Guid eventId);
}