using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;

namespace GatePassAPI.Repositories.EventRegistrationRepository;

public class EventRegistrationRepository (AppDatabaseContext databaseContext) : IEventRegistrationRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<EventRegistration> Upsert(EventRegistration eventRegistration)
	{
		_databaseContext.EventRegistrations.Add(eventRegistration);
		await _databaseContext.SaveChangesAsync();
		return eventRegistration;
	}
}