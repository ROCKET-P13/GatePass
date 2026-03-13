using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.EventClassRepository.Interfaces;

public interface IEventClassRepository
{
	public Task<EventClass> Upsert(EventClass eventClass);
}