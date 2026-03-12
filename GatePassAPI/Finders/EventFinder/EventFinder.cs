using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.EventFinder.Interfaces;
using GatePassAPI.Finders.EventFinder.DTOs;

namespace GatePassAPI.Finders.EventFinder;

public class EventFinder(AppDatabaseContext databaseContext) : IEventFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<EventViewModel>> GetAll()
	{
		return await _databaseContext.Events
			.Select(e => new EventViewModel
			{
				Id = e.Id,
				Name = e.Name,
				StartDateTime = e.StartDateTime,
				Status = e.Status,
				ParticipantCapacity = e.ParticipantCapacity,
			})
			.ToListAsync();
	}

	public async Task<List<EventViewModel>> GetByDateRange(Guid venueId, DateTime start, DateTime end)
	{
		return await _databaseContext.Events
			.Where(e => 
				e.VenueId == venueId &&
				e.StartDateTime >= start &&
				e.StartDateTime < end
			)
			.Select(e => new EventViewModel
			{
				Id = e.Id,
				Name = e.Name,
				StartDateTime = e.StartDateTime,
				Status = e.Status,
				ParticipantCapacity = e.ParticipantCapacity,
			})
			.OrderBy(e => e.StartDateTime)
			.ToListAsync();
	}

	public async Task<Event?> GetById(Guid eventId)
	{
		return await _databaseContext.Events
			.FirstOrDefaultAsync(e =>
				e.Id == eventId
			);
	}
}