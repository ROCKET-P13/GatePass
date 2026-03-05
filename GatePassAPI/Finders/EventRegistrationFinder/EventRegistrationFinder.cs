using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.EventRegistrationFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.EventRegistrationFinder;

public class EventRegistrationFinder(AppDatabaseContext databaseContext) : IEventRegistrationFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<EventRegistration>> GetByEventId(Guid eventId)
	{
		return await _databaseContext.EventRegistrations
			.Where(r => r.EventId == eventId)
			.Include(r => r.Participant)
			.ToListAsync();
	}
}