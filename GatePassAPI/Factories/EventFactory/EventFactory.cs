using GatePassAPI.Entities;
using GatePassAPI.Factories.EventFactory.DTOs;
using GatePassAPI.Factories.EventFactory.Interfaces;

namespace GatePassAPI.Factories.EventFactory;

public class EventFactory : IEventFactory
{
	public Event FromDto(EventFactoryDTO dto)
	{
		return new Event
		{
			Id = Guid.NewGuid(),
			VenueId = dto.VenueId,
			Name = dto.Name,
			Date = dto.Date,
			ParticipantCapacity = dto.ParticipantCapacity,
			Status = dto.Status
		};
	}
}