using GatePassAPI.Entities;
using GatePassAPI.Factories.EventClassFactory.DTOs;

namespace GatePassAPI.Factories.EventClassFactory.Interfaces;

public interface IEventClassFactory
{
	public EventClass FromDto (EventClassFactoryDTO dto);
}