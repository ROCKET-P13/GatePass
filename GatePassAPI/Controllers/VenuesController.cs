using Microsoft.AspNetCore.Mvc;
using GatePassAPI.DTOs.Requests;
using GatePassAPI.Factories.VenueFactory.DTOs;
using GatePassAPI.Factories.VenueFactory.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.VenueRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GatePassAPI.Controllers;

[Route("api/[controller]")]
public class VenuesController
(
	IVenueFactory venueFactory,
	IVenueRepository venueRepository,
	IVenueFinder venueFinder
) : ControllerBase
{
	private readonly IVenueFactory _venueFactory = venueFactory;
	private readonly IVenueRepository _venueRepository = venueRepository;
	private readonly IVenueFinder _venueFinder = venueFinder;

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
		var venueEntity = _venueFactory.FromDto(
			new VenueFactoryDTO
			{
				Name = request.Name,
				Sport = request.Sport,
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

		return Ok(venue);
	}

}