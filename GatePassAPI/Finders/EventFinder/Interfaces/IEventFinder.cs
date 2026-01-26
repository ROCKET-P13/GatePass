using GatePassAPI.Entities;

namespace GatePassAPI.Finders.EventFinder.Interfaces;

public interface IEventFinder
{
	Task<List<Event>> GetAll();
}