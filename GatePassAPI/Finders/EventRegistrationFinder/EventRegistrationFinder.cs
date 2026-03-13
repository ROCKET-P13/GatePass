using GatePassAPI.Data;
using GatePassAPI.Finders.EventRegistrationFinder.DTOs;
using GatePassAPI.Finders.EventRegistrationFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.EventRegistrationFinder;

public class EventRegistrationFinder(AppDatabaseContext databaseContext) : IEventRegistrationFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<EventRegistrationViewModel>> GetByEventId(Guid eventId)
	{
		return await _databaseContext.EventRegistrations
			.Where(r => r.EventId == eventId)
			.Include(r => r.Participant)
			.Select(r => new EventRegistrationViewModel
			{
				Id = r.Id,
				ParticipantId = r.ParticipantId,
				ParticipantFirstName = r.Participant.FirstName,
				ParticipantLastName = r.Participant.LastName,
				EventClassId = r.EventClassId,
				EventNumber = r.EventNumber,
				CheckedIn = r.CheckedIn,
				CreatedAt = r.CreatedAt,
				CheckedInAt = r.CheckedInAt,
				Type = r.Type
			})
			.ToListAsync();
	}
}