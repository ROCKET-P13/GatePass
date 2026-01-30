using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.UserRepository.Interfaces;

namespace GatePassAPI.Repositories.UserRepository;

public class UserRepository(AppDatabaseContext databaseContext) : IUserRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<User> Upsert(User user)
	{
		_databaseContext.Users.Add(user);
		await _databaseContext.SaveChangesAsync();
		return user;
	}
}