using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventClassRepository.Interfaces;

namespace GatePassAPI.Repositories.EventClassRepository;

public class EventClassRepository (AppDatabaseContext databaseContext) : IEventClassRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<EventClass> Upsert(EventClass eventClass)
	{
		_databaseContext.EventClasses.Add(eventClass);
		await _databaseContext.SaveChangesAsync();
		return eventClass;
	}
}