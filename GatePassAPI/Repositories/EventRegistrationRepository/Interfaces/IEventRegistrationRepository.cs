using GatePassAPI.Entities;
using GatePassAPI.Repositories.EventRegistrationRepository.DTOs;

namespace GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;

public interface IEventRegistrationRepository
{
	public Task<EventRegistration> Upsert(EventRegistration eventRegistration);
	public Task<EventRegistration?> GetBy(EventRegistrationRepositoryGetByDTO dto);
}