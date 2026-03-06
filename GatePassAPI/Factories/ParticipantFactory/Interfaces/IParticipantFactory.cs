using GatePassAPI.Entities;
using GatePassAPI.Factories.ParticipantFactory.DTOs;

namespace GatePassAPI.Factories.ParticipantFactory.Interfaces;

public interface IParticipantFactory
{
	public Participant FromDto(ParticipantFactoryDTO dto);
}