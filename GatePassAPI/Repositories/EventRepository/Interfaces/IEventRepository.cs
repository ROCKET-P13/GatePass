using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.EventRepository.Interfaces;

public interface IEventRepository
{
	public Task<Event> Upsert(Event eventEntity);
	public Task Delete(Guid id);
	public Task<Event?> FindById (Guid eventId, Guid? venueId);
	public Task<Event> Update(Event eventEntity);
};