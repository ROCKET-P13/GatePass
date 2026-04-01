using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRegistrationRepository.DTOs;
using GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Repositories.EventRegistrationRepository;

public class EventRegistrationRepository (AppDatabaseContext databaseContext) : IEventRegistrationRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<EventRegistration> Upsert(EventRegistration eventRegistration)
	{
		var existingEventRegistration = await _databaseContext.EventRegistrations
		.FirstOrDefaultAsync(registration => registration.Id == eventRegistration.Id);

		if (existingEventRegistration == null)
		{
			_databaseContext.EventRegistrations.Add(eventRegistration);
		}
		else
		{
			_databaseContext.Entry(existingEventRegistration)
				.CurrentValues
				.SetValues(eventRegistration);
		}

		await _databaseContext.SaveChangesAsync();
		return eventRegistration;
	}

	public async Task<EventRegistration?> GetBy(EventRegistrationRepositoryGetByDTO dto)
	{
		return await _databaseContext.EventRegistrations
			.FirstOrDefaultAsync(registration => registration.ParticipantId == dto.ParticipantId && registration.EventId == dto.EventId);
	}
}