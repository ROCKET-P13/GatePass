using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Repositories.EventRepository;

public class EventRepository(AppDatabaseContext databaseContext) : IEventRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Event> Upsert (Event eventEntity)
	{
		_databaseContext.Events.Add(eventEntity);
		await _databaseContext.SaveChangesAsync();
		return eventEntity;
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

	public async Task<Event?> FindById (Guid id, Guid? venueId)
	{
		return await _databaseContext.Events
			.FirstOrDefaultAsync(e => e.Id == id && e.VenueId == venueId);
	}

	public async Task<Event> Update (Event eventEntity)
	{
		_databaseContext.Events.Update(eventEntity);
		await _databaseContext.SaveChangesAsync();
		return eventEntity;
	}

}