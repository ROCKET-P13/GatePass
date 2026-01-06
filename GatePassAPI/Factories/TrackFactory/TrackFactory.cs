using MotoPassAPI.Entities;
using MotoPassAPI.Factories.TrackFactory.DTOs;
using MotoPassAPI.Factories.TrackFactory.Interfaces;

namespace MotoPassAPI.Factories.TrackFactory;

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