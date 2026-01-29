using GatePassAPI.Entities;
using GatePassAPI.Factories.EventFactory.DTOs;

namespace GatePassAPI.Factories.EventFactory.Interfaces;

public interface IEventFactory
{
	public Event FromDto(EventFactoryDTO dto);
}