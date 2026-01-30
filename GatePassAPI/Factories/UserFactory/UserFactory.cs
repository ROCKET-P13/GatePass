using GatePassAPI.Entities;
using GatePassAPI.Factories.UserFactory.DTOs;
using GatePassAPI.Factories.UserFactory.Interfaces;

namespace GatePassAPI.Factories.UserFactory;

public class UserFactory : IUserFactory
{
	public User FromDto(UserFactoryDTO dto)
	{
		return new User
		{
			Id = Guid.NewGuid(),
			Auth0Id = dto.Auth0Id,
			VenueId = dto.VenueId,
			Email = dto.Email,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			CreatedAt = DateTime.UtcNow
		};
	}
}