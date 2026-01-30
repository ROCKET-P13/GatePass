using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.UserFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.UserFinder;

public class UserFinder(AppDatabaseContext databaseContext) : IUserFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<User?> GetByAuth0Id(string auth0UserId)
	{
		return await _databaseContext.Users
			.FirstOrDefaultAsync(user => user.Auth0Id == auth0UserId);
	}
}