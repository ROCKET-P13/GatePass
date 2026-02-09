using System.Security.Claims;
using GatePassAPI.Factories.UserFactory.DTOs;
using GatePassAPI.Factories.UserFactory.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.UserRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController
(
	IUserFinder userFinder,
	IUserFactory userFactory,
	IUserRepository userRepository,
	IVenueFinder venueFinder
) : ControllerBase
{
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IUserFactory _userFactory = userFactory;
	private readonly IUserRepository _userRepository = userRepository;
	private readonly IVenueFinder _venueFinder = venueFinder;

	[Authorize]
	[HttpGet("me")]
	public async Task<IActionResult> GetCurrentUser()
	{
		var auth0Id = User.FindFirstValue("sub");

		if (string.IsNullOrWhiteSpace(auth0Id))
		{
			return Unauthorized();
		}

		var email = User.FindFirstValue("email");
		if (email == null)
		{
			return BadRequest("Email claim missing");
		}


		var firstNameClaim = User.FindFirstValue("given_name") ?? "";
		var lastNameClaim = User.FindFirstValue("family_name") ?? "";

		var user = await _userFinder.GetByAuth0Id(auth0Id);

		if (user == null)
		{
			user = _userFactory.FromDto(new UserFactoryDTO
				{
					Auth0Id = auth0Id,
					Email = email,
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

	[Authorize]
	[HttpGet("venue")]
	public async Task<IActionResult> GetVenue()
	{
		var auth0Id = User.FindFirstValue("sub");

		if (string.IsNullOrWhiteSpace(auth0Id))
		{
			return Unauthorized();
		}

		var user = await _userFinder.GetByAuth0Id(auth0Id);

		if (user == null)
		{
			return NotFound("User not found");
		}
		
		if (user.VenueId == null)
		{
			return NotFound("User is not assigned to a venue");
		}

		var venue = await _venueFinder.GetById(user.VenueId);

		if (venue == null)
		{
			return NotFound("Venue not found");
		}

		return Ok(venue);
	}
}