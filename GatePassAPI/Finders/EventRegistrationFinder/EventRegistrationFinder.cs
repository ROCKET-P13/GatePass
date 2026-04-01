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
			.GroupJoin(
				_databaseContext.EventClasses,
				registration => registration.EventClassId,
				eventClass => eventClass.Id,
				(registration, eventClasses) => new { registration, eventClasses }
			)
			.SelectMany(
				x => x.eventClasses.DefaultIfEmpty(),
				(x, eventClass) => new EventRegistrationViewModel
				{
					Id = x.registration.Id,
					ParticipantId = x.registration.ParticipantId,
					ParticipantFirstName = x.registration.Participant.FirstName,
					ParticipantLastName = x.registration.Participant.LastName,
					EventClassId = x.registration.EventClassId,
					EventClassName = eventClass != null ? eventClass.Name : null,
					EventNumber = x.registration.EventNumber,
					CheckedIn = x.registration.CheckedIn,
					CheckedInAt = x.registration.CheckedInAt,
					Type = x.registration.Type,
				}
			).ToListAsync();
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

	public async Task<List<EventRegistrationViewModel>> GetCheckinsByEventId(Guid eventId)
	{
		return await _databaseContext.EventRegistrations
			.Where(registration => registration.EventId == eventId)
			.Include(registration => registration.Participant)
			.GroupJoin(
				_databaseContext.EventClasses,
				registration => registration.EventClassId,
				eventClass => eventClass.Id,
				(registration, eventClasses) => new { registration, eventClasses }
			)
			.SelectMany(
				x => x.eventClasses.DefaultIfEmpty(),
				(x, eventClass) => new EventRegistrationViewModel
				{
					Id = x.registration.Id,
					ParticipantId = x.registration.ParticipantId,
					ParticipantFirstName = x.registration.Participant.FirstName,
					ParticipantLastName = x.registration.Participant.LastName,
					EventClassId = x.registration.EventClassId,
					EventClassName = eventClass != null ? eventClass.Name : null,
					EventNumber = x.registration.EventNumber,
					CheckedIn = x.registration.CheckedIn,
					CheckedInAt = x.registration.CheckedInAt,
					Type = x.registration.Type,
				}
			)
			.ToListAsync();
	}
}