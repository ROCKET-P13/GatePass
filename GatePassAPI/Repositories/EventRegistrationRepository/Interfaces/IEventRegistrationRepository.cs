using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;

public interface IEventRegistrationRepository
{
	public Task<EventRegistration> Upsert(EventRegistration eventRegistration);
}