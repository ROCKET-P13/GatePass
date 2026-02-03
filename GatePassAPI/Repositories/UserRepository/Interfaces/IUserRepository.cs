using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.UserRepository.Interfaces;

public interface IUserRepository
{
	public Task<User?> FindByAuth0Id(string auth0Id);
	public Task<User> Upsert(User user);
}