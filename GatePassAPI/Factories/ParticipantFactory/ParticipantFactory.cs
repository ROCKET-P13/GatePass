using GatePassAPI.Entities;
using GatePassAPI.Factories.ParticipantFactory.DTOs;
using GatePassAPI.Factories.ParticipantFactory.Interfaces;

namespace GatePassAPI.Factories.ParticipantFactory;

public class ParticipantFactory : IParticipantFactory
{
	public Participant FromDto(ParticipantFactoryDTO dto)
	{
		return new Participant
		{
			Id = Guid.NewGuid(),
			VenueId = dto.VenueId,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			CreatedAt = DateTime.UtcNow
		};
	}
}