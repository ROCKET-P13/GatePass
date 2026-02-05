using Microsoft.AspNetCore.Mvc;
using GatePassAPI.DTOs.Requests;
using GatePassAPI.Factories.VenueFactory.DTOs;
using GatePassAPI.Factories.VenueFactory.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.VenueRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GatePassAPI.Repositories.UserRepository.Interfaces;

namespace GatePassAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
public class VenuesController
(
	IVenueFactory venueFactory,
	IVenueRepository venueRepository,
	IVenueFinder venueFinder,
	IUserRepository userRepository
) : ControllerBase
{
	private readonly IVenueFactory _venueFactory = venueFactory;
	private readonly IVenueRepository _venueRepository = venueRepository;
	private readonly IVenueFinder _venueFinder = venueFinder;
	private readonly IUserRepository _userRepository = userRepository;
	
	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var venues = await _venueFinder.GetAll();
		return Ok(venues);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddVenueRequest request)
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

		var user = await _userRepository.FindByAuth0Id(auth0Id);

		if (user == null)
		{
			return Unauthorized("User not found.");
		}

		if (user.VenueId != null)
		{
			return BadRequest("User already belongs to a venue");
		}

		var venueEntity = _venueFactory.FromDto(
			new VenueFactoryDTO
			{
				Name = request.Name,
				LogoImageURL = request.LogoImageURL,
				PhoneNumber = request.PhoneNumber,
				AddressLine1 = request.AddressLine1,
				AddressLine2 = request.AddressLine2,
				City = request.City,
				State = request.State,
				Country = request.Country
			}
		);

		var venue = await _venueRepository.Upsert(venueEntity);

		user.AssignToVenue(venue.Id);
		await _userRepository.Upsert(user);

		return Ok(venue);
	}

}