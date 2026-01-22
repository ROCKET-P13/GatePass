using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.EventRepository.Interfaces;

public interface IEventRepository
{
	public Task<Event> Upsert(Event venueEvent);
};