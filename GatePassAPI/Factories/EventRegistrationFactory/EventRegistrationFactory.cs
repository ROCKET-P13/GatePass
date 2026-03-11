using GatePassAPI.Entities;
using GatePassAPI.Factories.EventRegistrationFactory.DTOs;
using GatePassAPI.Factories.EventRegistrationFactory.Interfaces;

namespace GatePassAPI.Factories.EventRegistrationFactory;

public class EventRegistrationFactory : IEventRegistrationFactory
{
	public EventRegistration Create(EventRegistrationFactoryCreateDTO dto)
	{
		return new EventRegistration
		{
			Id = Guid.NewGuid(),
			EventId = dto.EventId,
			ParticipantId = dto.ParticipantId,
			EventNumber = dto.EventNumber,
			CheckedIn = dto.CheckedIn,
			CreatedAt = DateTime.UtcNow
		};
	}
}