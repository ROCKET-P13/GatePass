using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.UserRepository.Interfaces;

public interface IUserRepository
{
	public Task<User> Upsert(User user);
}