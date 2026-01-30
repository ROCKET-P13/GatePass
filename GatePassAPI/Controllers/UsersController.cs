using System.Security.Claims;
using GatePassAPI.Factories.UserFactory.DTOs;
using GatePassAPI.Factories.UserFactory.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Repositories.UserRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController
(
	IUserFinder userFinder,
	IUserFactory userFactory,
	IUserRepository userRepository
) : ControllerBase
{
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IUserFactory _userFactory = userFactory;
	private readonly IUserRepository _userRepository = userRepository;

	[Authorize]
	[HttpGet("me")]
	public async Task<IActionResult> GetCurrentUser()
	{
		var auth0Id = User.FindFirstValue(ClaimTypes.NameIdentifier)
			?? User.FindFirstValue("sub");

		if (auth0Id == null)
		{
			return Unauthorized();
		}

		var user = await _userFinder.GetByAuth0Id(auth0Id);

		var subClaim = User.FindFirstValue("sub");
		var emailClaim = User.FindFirstValue("email");
		var firstNameClaim = User.FindFirstValue("given_name");
		var lastNameClaim = User.FindFirstValue("family_name");

		if (subClaim == null || emailClaim == null || firstNameClaim == null || lastNameClaim == null)
		{
			return BadRequest("Missing required user claims");
		}

		if (user == null)
		{
			user = _userFactory.FromDto(new UserFactoryDTO
				{
					Auth0Id = subClaim,
					VenueId = null,
					Email = emailClaim,
					FirstName = firstNameClaim,
					LastName = lastNameClaim,
				});

			await _userRepository.Upsert(user);

		}


		return Ok(new
		{
			user.Id,
			user.Email,
			user.FirstName,
			user.LastName,
			user.VenueId
		});
	}
}