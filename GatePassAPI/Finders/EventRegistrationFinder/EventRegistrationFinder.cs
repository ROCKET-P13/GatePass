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
			.Where(registration => registration.EventId == eventId)
			.Include(registration => registration.Participant)
			.Select(registration => new EventRegistrationViewModel
			{
				Id = registration.Id,
				ParticipantId = registration.ParticipantId,
				ParticipantFirstName = registration.Participant.FirstName,
				ParticipantLastName = registration.Participant.LastName,
				EventClassId = registration.EventClassId,
				EventNumber = registration.EventNumber,
				CheckedIn = registration.CheckedIn,
				CreatedAt = registration.CreatedAt,
				CheckedInAt = registration.CheckedInAt,
				Type = registration.Type
			})
			.ToListAsync();
	}

	public async Task<List<ParticipantRegistrationViewModel>> GetByParticipantId(Guid participantId)
	{
		return await _databaseContext.EventRegistrations
			.Where(registration => registration.ParticipantId == participantId)
			.Join(
				_databaseContext.Events,
				registration => registration.EventId,
				venueEvent => venueEvent.Id,
				(registration, venueEvent) => new ParticipantRegistrationViewModel
					{
						Id = registration.Id,
						EventId = registration.EventId,
						EventDate = venueEvent.StartDateTime,
						EventName = venueEvent.Name,
						Type = registration.Type,
						EventNumber = registration.EventNumber,
						CreatedAt = registration.CreatedAt,
						CheckedIn = registration.CheckedIn,
						CheckedInAt = registration.CheckedInAt
					}
			)
			.ToListAsync();
	}

	public async Task<List<ParticipantRegistrationViewModel>> GetCheckinsByEventId(Guid eventId)
	{
		return await _databaseContext.EventRegistrations
			.Where(registration => registration.EventId == eventId && registration.CheckedIn)
			.Join(
				_databaseContext.Events,
				registration => registration.EventId,
				venueEvent => venueEvent.Id,
				(registration, venueEvent) => new ParticipantRegistrationViewModel
					{
						Id = registration.Id,
						EventId = registration.EventId,
						EventDate = venueEvent.StartDateTime,
						EventName = venueEvent.Name,
						Type = registration.Type,
						EventNumber = registration.EventNumber,
						CreatedAt = registration.CreatedAt,
						CheckedIn = registration.CheckedIn,
						CheckedInAt = registration.CheckedInAt
					}
			)
			.ToListAsync();
	}
}