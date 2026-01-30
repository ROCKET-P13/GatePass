using GatePassAPI.Entities;

namespace GatePassAPI.Finders.UserFinder.Interfaces;

public interface IUserFinder
{
	Task<User?> GetByAuth0Id(string auth0UserId);
}