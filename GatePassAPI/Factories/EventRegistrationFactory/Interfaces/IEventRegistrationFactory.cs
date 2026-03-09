using GatePassAPI.Entities;

namespace GatePassAPI.Factories.EventRegistrationFactory.Interfaces;

public interface IEventRegistrationFactory
{
	public EventRegistration Create(Guid participantId, Guid eventId);
}