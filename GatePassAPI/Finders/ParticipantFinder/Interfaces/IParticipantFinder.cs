using GatePassAPI.Finders.ParticipantFinder.DTOs;

namespace GatePassAPI.Finders.ParticipantFinder.Interfaces;

public interface IParticipantFinder
{
	Task<List<ParticipantViewModel>> GetAll(Guid venueId);	
}