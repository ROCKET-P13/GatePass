using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRepository.Interfaces;

namespace GatePassAPI.Repositories.EventRepository;

public class EventRepository(AppDatabaseContext databaseContext) : IEventRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Event> Upsert (Event venueEvent)
	{
		_databaseContext.Events.Add(venueEvent);
		await _databaseContext.SaveChangesAsync();
		return venueEvent;
	}
}