using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.UserRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Repositories.UserRepository;

public class UserRepository(AppDatabaseContext databaseContext) : IUserRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<User?> FindByAuth0Id(string auth0Id)
	{
		return await _databaseContext.Users
			.FirstOrDefaultAsync(user => user.Auth0Id == auth0Id);
	}
	public async Task<User> Upsert(User user)
	{
		var existingUser = await _databaseContext.Users
			.FirstOrDefaultAsync(user => user.Auth0Id == user.Auth0Id);

		if (existingUser == null)
		{
			_databaseContext.Users.Add(user);
		}
		else
		{
			_databaseContext.Entry(existingUser)
				.CurrentValues
				.SetValues(user);

			user = existingUser;
		}
		
		await _databaseContext.SaveChangesAsync();
		return user;
	}
}