using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

	public async Task Delete (Guid id)
	{
		var eventEntity = await _databaseContext.Events
			.FirstOrDefaultAsync(e => e.Id == id);

		if (eventEntity == null)
		{
			return;
		}

		_databaseContext.Events.Remove(eventEntity);

		await _databaseContext.SaveChangesAsync();
	}

}