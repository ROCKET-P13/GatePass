
using Microsoft.AspNetCore.Mvc;
using MotoPassAPI.DTOs.Requests;
using MotoPassAPI.Factories.TrackFactory.DTOs;
using MotoPassAPI.Factories.TrackFactory.Interfaces;
using MotoPassAPI.Finders.TrackFinder.Interfaces;
using MotoPassAPI.Repositories.Interfaces;

namespace MotoPassAPI.Controllers;

[Route("api/[controller]")]
public class TracksController
(
	ITrackFactory trackFactory,
	ITrackRepository trackRepository,
	ITrackFinder trackFinder
) : ControllerBase
{
	private readonly ITrackFactory _trackFactory = trackFactory;
	private readonly ITrackRepository _trackRepository = trackRepository;
	private readonly ITrackFinder _trackFinder = trackFinder;

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var tracks = await _trackFinder.GetAll();
		return Ok(tracks);
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddTrackRequest request)
	{
		var trackEntity = _trackFactory.FromDto(
			new TrackFactoryDTO
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

		var track = await _trackRepository.Upsert(trackEntity);

		return Ok(track);
	}

}