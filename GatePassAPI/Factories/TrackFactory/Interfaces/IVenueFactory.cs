using GatePassAPI.Entities;
using GatePassAPI.Factories.VenueFactory.DTOs;

namespace GatePassAPI.Factories.VenueFactory.Interfaces;

public interface IVenueFactory
{
	public Venue FromDto(VenueFactoryDTO dto);
}