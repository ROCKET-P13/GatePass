using System.Security.Claims;
using GatePassAPI.Controllers.DTOs;
using GatePassAPI.Factories.ParticipantFactory.DTOs;
using GatePassAPI.Factories.ParticipantFactory.Interfaces;
using GatePassAPI.Finders.ParticipantFinder.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.ParticipantRepository.Interfaces;
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
	IParticipantFinder participantFinder,
	IParticipantFactory participantFactory,
	IParticipantRepository participantRepository
) : ControllerBase
{
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IVenueFinder _venueFinder = venueFinder;
	private readonly IParticipantFinder _participantFinder = participantFinder;
	private readonly IParticipantFactory _participantFactory = participantFactory;
	private readonly IParticipantRepository _participantRepository = participantRepository;


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

	[Authorize]
	[HttpGet("{participantId:guid}")]
	public async Task<IActionResult> GetById (Guid participantId)
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

		var participant = await _participantFinder.GetById(participantId);
		return Ok(participant);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddParticipantRequest request)
	{
		if (request == null)
		{
			return BadRequest("Request body missing");
		}

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

		var participantEntity = _participantFactory.FromDto(
			new ParticipantFactoryDTO
			{
				VenueId = venue.Id,
				FirstName = request.FirstName,
				LastName = request.LastName
			}
		);

		var participant = await _participantRepository.Upsert(participantEntity);

		return Ok(new
		{
			participant.Id,
			participant.FirstName,
			participant.LastName,
			participant.CreatedAt
		});
	}
} 