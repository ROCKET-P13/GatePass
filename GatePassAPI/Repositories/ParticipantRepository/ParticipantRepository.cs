using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.ParticipantRepository.Interfaces;

namespace GatePassAPI.Repositories.ParticipantRepository;

public class ParticipantRepository(AppDatabaseContext databaseContext) : IParticipantRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Participant> Upsert(Participant participant)
	{
		_databaseContext.Participants.Add(participant);
		await _databaseContext.SaveChangesAsync();
		return participant;
	}
}