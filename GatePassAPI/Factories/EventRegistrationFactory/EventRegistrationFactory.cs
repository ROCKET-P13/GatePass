using GatePassAPI.Entities;
using GatePassAPI.Factories.EventRegistrationFactory.Interfaces;

namespace GatePassAPI.Factories.EventRegistrationFactory;

public class EventRegistrationFactory : IEventRegistrationFactory
{
	public EventRegistration Create(Guid participantId, Guid eventId)
	{
		return new EventRegistration
		{
			Id = Guid.NewGuid(),
			EventId = eventId,
			ParticipantId = participantId,
			CheckedIn = false,
			CreatedAt = DateTime.UtcNow
		};
	}
}