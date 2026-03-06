using System.Security.Claims;
using GatePassAPI.Finders.ParticipantFinder.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class ParticipantsController
(
	IUserFinder userFinder,
	IVenueFinder venueFinder,
	IParticipantFinder participantFinder
) : ControllerBase
{
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IVenueFinder _venueFinder = venueFinder;
	private readonly IParticipantFinder _participantFinder = participantFinder;


	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
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

		var participants = await _participantFinder.GetAll(venue.Id);
		return Ok(participants);
	}
} 