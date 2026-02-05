using GatePassAPI.Entities;
using GatePassAPI.Factories.VenueFactory.DTOs;
using GatePassAPI.Factories.VenueFactory.Interfaces;

namespace GatePassAPI.Factories.VenueFactory;

public class VenueFactory : IVenueFactory
{
	public Venue FromDto(VenueFactoryDTO dto)
	{
		Console.WriteLine(dto);
		return new Venue
		{
			Id = Guid.NewGuid(),
			Name = dto.Name,
			LogoImageURL = dto.LogoImageURL,
			PhoneNumber = dto.PhoneNumber,
			AddressLine1 = dto.AddressLine1,
			AddressLine2 = dto.AddressLine2,
			City = dto.City,
			State = dto.State,
			Country = dto.Country
		};
	}
}