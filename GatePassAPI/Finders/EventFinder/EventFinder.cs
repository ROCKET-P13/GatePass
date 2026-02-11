using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.EventFinder.Interfaces;

namespace GatePassAPI.Finders.EventFinder;

public class EventFinder(AppDatabaseContext databaseContext) : IEventFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<Event>> GetAll()
	{
		return await _databaseContext.Events.ToListAsync();
	}

	public async Task<List<Event>> GetByDateRange(Guid venueId, DateTime start, DateTime end)
	{
		return await _databaseContext.Events
		.Where(e => 
			e.VenueId == venueId &&
			e.StartDateTime >= start &&
			e.StartDateTime < end
		)
		.OrderBy(e => e.StartDateTime)
		.ToListAsync();
	}
}