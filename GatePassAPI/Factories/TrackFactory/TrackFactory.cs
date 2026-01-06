using GatePassAPI.Entities;
using GatePassAPI.Factories.TrackFactory.DTOs;
using GatePassAPI.Factories.TrackFactory.Interfaces;

namespace GatePassAPI.Factories.TrackFactory;

public class TrackFactory : ITrackFactory
{
	public Track FromDto(TrackFactoryDTO dto)
	{
		return new Track
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