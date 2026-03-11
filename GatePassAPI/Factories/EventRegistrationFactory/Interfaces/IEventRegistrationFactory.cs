using GatePassAPI.Entities;
using GatePassAPI.Factories.EventRegistrationFactory.DTOs;

namespace GatePassAPI.Factories.EventRegistrationFactory.Interfaces;

public interface IEventRegistrationFactory
{
	public EventRegistration Create(EventRegistrationFactoryCreateDTO dto);
}