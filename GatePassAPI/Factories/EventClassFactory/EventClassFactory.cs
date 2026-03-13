using GatePassAPI.Entities;
using GatePassAPI.Factories.EventClassFactory.DTOs;
using GatePassAPI.Factories.EventClassFactory.Interfaces;

namespace GatePassAPI.Factories.EventClassFactory;

public class EventClassFactory : IEventClassFactory
{
	public EventClass FromDto(EventClassFactoryDTO dto)
	{
		return new EventClass
		{
			Id = Guid.NewGuid(),
			Name = dto.Name,
			EventId = dto.EventId,
			MaximumAge = dto.MaximumAge,
			MinimumAge = dto.MinimumAge,
			SkillLevel = dto.SkillLevel,
			Gender = dto.Gender,
			ParticipantCapacity = dto.ParticipantCapacity
		};
	}
}