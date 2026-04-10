using GatePassAPI.Entities;
using GatePassAPI.Finders.EventClassFinder.DTOs;

namespace GatePassAPI.Finders.EventClassFinder.Interfaces;

public interface IEventClassFinder
{
	public Task<List<EventClassViewModel>> GetByEventId (Guid eventId);
	public Task<EventClass?> GetById (Guid id);
}