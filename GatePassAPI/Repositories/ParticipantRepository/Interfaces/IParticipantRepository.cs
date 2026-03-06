using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.ParticipantRepository.Interfaces;

public interface IParticipantRepository
{
	public Task<Participant> Upsert(Participant participant);
}