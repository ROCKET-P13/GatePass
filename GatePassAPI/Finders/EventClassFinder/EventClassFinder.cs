using GatePassAPI.Data;
using GatePassAPI.Finders.EventClassFinder.DTOs;
using GatePassAPI.Finders.EventClassFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.EventClassFinder;

public class EventClassFinder(AppDatabaseContext databaseContext) : IEventClassFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;
	public async Task<List<EventClassViewModel>> GetByEventId(Guid eventId)
	{
		return await _databaseContext.EventClasses
			.Where(eventClass => eventClass.EventId == eventId)
			.Select(eventClass => new EventClassViewModel
			{
				Id = eventClass.Id,
				Name = eventClass.Name,
				MaximumAge = eventClass.MaximumAge,
				MinimumAge = eventClass.MinimumAge,
				SkillLevel = eventClass.SkillLevel,
				Gender = eventClass.Gender,
				ParticipantCapacity = eventClass.ParticipantCapacity,
				StartTime = eventClass.StartTime
			})
			.ToListAsync();
	}
}