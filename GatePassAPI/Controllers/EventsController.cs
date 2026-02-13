using System.Security.Claims;
using GatePassAPI.DTOs.Requests;
using GatePassAPI.Entities;
using GatePassAPI.Enums;
using GatePassAPI.Factories.EventFactory.DTOs;
using GatePassAPI.Factories.EventFactory.Interfaces;
using GatePassAPI.Finders.EventFinder.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.EventRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventsController
(
	IEventFinder eventFinder,
	IEventFactory eventFactory,
	IEventRepository eventRepository,
	IUserFinder userFinder,
	IVenueFinder venueFinder
) : ControllerBase
{
	private readonly IEventFinder _eventFinder = eventFinder;
	private readonly IEventFactory _eventFactory = eventFactory;
	private readonly IEventRepository _eventRepository = eventRepository;
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IVenueFinder _venueFinder = venueFinder;

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var events = await _eventFinder.GetAll();
		return Ok(events);
	}

	[Authorize]
	[HttpGet("today")]
	public async Task<IActionResult> GetTodays()
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

		var today = DateTime.UtcNow;
		var events = await _eventFinder.GetByDateRange(
			venue.Id,
			today,
			today.AddDays(1)
		);

		return Ok(events);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddEventRequest request)
	{

		if (request == null)
		{
			return BadRequest("Request body missing");
		}

		if (!Enum.IsDefined(typeof(EventStatus), request.Status))
		{
			return BadRequest("Invalid event status");
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

		var eventEntity = _eventFactory.FromDto(
			new EventFactoryDTO
			{
				VenueId = venue.Id,
				Name = request.Name,
				StartDateTime = request.StartDateTime,
				Status = request.Status,
				ParticipantCapacity = request.ParticipantCapacity
			}
		);

		var newEvent = await _eventRepository.Upsert(eventEntity);

		return Ok(newEvent);

	}

	[Authorize]
	[HttpPatch("{eventId}/status")]
	public async Task<IActionResult> UpdateStatus(Guid eventId, [FromBody] UpdateEventRequest request)
	{
		if (!Enum.IsDefined(typeof(EventStatus), request.Status))
		{
			return BadRequest("Invalid Status");
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
			return BadRequest("User is not assigned to a venue");
		}

		var eventEntity = await _eventFinder.GetById(user.VenueId.Value, eventId);
		return Ok(eventEntity);

	}
}