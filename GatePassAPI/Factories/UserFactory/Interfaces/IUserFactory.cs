using GatePassAPI.Entities;
using GatePassAPI.Factories.UserFactory.DTOs;

namespace GatePassAPI.Factories.UserFactory.Interfaces;

public interface IUserFactory
{
	public User FromDto(UserFactoryDTO dto);
}