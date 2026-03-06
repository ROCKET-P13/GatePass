using GatePassAPI.Entities;

namespace GatePassAPI.Finders.ParticipantFinder.Interfaces;

public interface IParticipantFinder
{
	Task<List<Participant>> GetAll(Guid venueId);	
}