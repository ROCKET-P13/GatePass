using System.Security.Claims;
using GatePassAPI.Controllers.DTOs;
using GatePassAPI.Enums;
using GatePassAPI.Factories.EventClassFactory.DTOs;
using GatePassAPI.Factories.EventClassFactory.Interfaces;
using GatePassAPI.Factories.EventFactory.DTOs;
using GatePassAPI.Factories.EventFactory.Interfaces;
using GatePassAPI.Factories.EventRegistrationFactory.DTOs;
using GatePassAPI.Factories.EventRegistrationFactory.Interfaces;
using GatePassAPI.Finders.EventFinder.Interfaces;
using GatePassAPI.Finders.EventRegistrationFinder.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.EventClassRepository.Interfaces;
using GatePassAPI.Repositories.EventRegistrationRepository.DTOs;
using GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;
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
	IVenueFinder venueFinder,
	IEventRegistrationFinder eventRegistrationFinder,
	IEventRegistrationFactory eventRegistrationFactory,
	IEventRegistrationRepository eventRegistrationRepository,
	IEventClassFactory eventClassFactory,
	IEventClassRepository eventClassRepository
) : ControllerBase
{
	private readonly IEventFinder _eventFinder = eventFinder;
	private readonly IEventFactory _eventFactory = eventFactory;
	private readonly IEventRepository _eventRepository = eventRepository;
	private readonly IUserFinder _userFinder = userFinder;
	private readonly IVenueFinder _venueFinder = venueFinder;
	private readonly IEventRegistrationFinder _eventRegistrationFinder = eventRegistrationFinder;
	private readonly IEventRegistrationFactory _eventRegistrationFactory = eventRegistrationFactory;
	private readonly IEventRegistrationRepository _eventRegistrationRepository = eventRegistrationRepository;
	private readonly IEventClassFactory _eventClassFactory = eventClassFactory;
	private readonly IEventClassRepository _eventClassRepository = eventClassRepository;

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var auth0Id = User.FindFirstValue("sub");

		if (string.IsNullOrWhiteSpace(auth0Id))
		{
			return Unauthorized();
		}

		var events = await _eventFinder.GetAll();
		return Ok(events);
	}

	[Authorize]
	[HttpGet("{eventId:guid}")]
	public async Task<IActionResult> GetById(Guid eventId)
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

		var requestedEvent = await _eventFinder.GetById(eventId);
		return Ok(requestedEvent);
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
	[HttpPatch("{eventId:guid}")]
	public async Task<IActionResult> Update(Guid eventId, [FromBody] UpdateEventRequest request)
	{

		if (request.Status != null && !Enum.IsDefined(typeof(EventStatus), request.Status))
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

		var eventEntity = await _eventRepository.FindById(eventId, user.VenueId);

		if (eventEntity == null)
		{
			return NotFound();
		}

		if (eventEntity.Status == EventStatus.Completed && request.Status != EventStatus.Completed)
		{
			return BadRequest("Completed events cannot change status");
		}

		eventEntity.Update(
			request.Name,
			request.StartDateTime,
			request.Status,
			request.ParticipantCapacity
		);

		var updatedEvent = await _eventRepository.Update(eventEntity);
		return Ok(updatedEvent);
	}

	[Authorize]
	[HttpDelete("{eventId:guid}")]
	public async Task<IActionResult> DeleteEvent(Guid eventId)
	{
		await ValidateRequestElseThrow(eventId);

		await _eventRepository.Delete(eventId);

		return NoContent();
	}

	[Authorize]
	[HttpGet("{eventId:guid}/registrations")]
	public async Task<IActionResult> GetRegistrations(Guid eventId)
	{
		await ValidateRequestElseThrow(eventId);

		var registrations = await _eventRegistrationFinder.GetByEventId(eventId);
		return Ok(registrations);
	}

	[Authorize]
	[HttpGet("{eventId:guid}/checkins")]
	public async Task<IActionResult> GetCheckins(Guid eventId)
	{
		await ValidateRequestElseThrow(eventId);

		var checkIns = await _eventRegistrationFinder.GetCheckinsByEventId(eventId);

		return Ok(checkIns);
	}

	[Authorize]
	[HttpPost("{eventId:guid}/registrations")]
	public async Task<IActionResult> RegisterParticipant(Guid eventId, [FromBody] RegisterParticipantRequest request)
	{
		await ValidateRequestElseThrow(eventId);

		var eventRegistration = _eventRegistrationFactory.Create(
			new EventRegistrationFactoryCreateDTO
			{
				EventId = eventId,
				ParticipantId = request.ParticipantId,
				EventNumber = request.EventNumber,
				EventClassId = request.EventClassId,
				CheckedIn = request.CheckedIn,
				Type = request.Type,
			}
		);

		await _eventRegistrationRepository.Upsert(eventRegistration);

		return Ok(eventRegistration);
	}

	[Authorize]
	[HttpPost("{eventId:guid}/classes")]
	public async Task<IActionResult> AddEventClass(Guid eventId, [FromBody] AddEventClassRequest request)
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
			return BadRequest("User is not assigned to a venue");
		}

		var eventEntity = await _eventFinder.GetById(eventId);

		if (eventEntity == null)
		{
			return NotFound();
		}

		if (eventEntity.VenueId != user.VenueId)
		{
			return Forbid();
		}

		if (
			request.Gender != Gender.Male
			&& request.Gender != Gender.Female
			&& request.Gender != Gender.Both
		)
		{
			return BadRequest("Gender is not valid");
		}

		var eventClass = _eventClassFactory.FromDto(new EventClassFactoryDTO
		{
			EventId = eventId,
			Name = request.Name,
			MaximumAge = request.MaximumAge,
			MinimumAge = request.MinimumAge,
			SkillLevel = request.SkillLevel,
			Gender = request.Gender,
			ParticipantCapacity = request.ParticipantCapacity
		});

		await _eventClassRepository.Upsert(eventClass);

		return Ok(eventClass);
	}

	[Authorize]
	[HttpPost("{eventId:guid}/checkins")]
	public async Task<IActionResult> CheckinParticipant(Guid eventId, [FromBody] CheckinParticipantRequest request)
	{
		await ValidateRequestElseThrow(eventId);

		var eventRegistration = await _eventRegistrationRepository.GetBy(new EventRegistrationRepositoryGetByDTO
		{
			EventId = eventId,
			ParticipantId = request.ParticipantId
		});

		if (eventRegistration == null)
		{
			return NotFound("Event Registration not found");
		}

		eventRegistration.CheckIn();

		await _eventRegistrationRepository.Upsert(eventRegistration);

		return Ok(eventRegistration);
	}

	private async Task<IActionResult?> ValidateRequestElseThrow(Guid eventId)
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
			return BadRequest("User is not assigned to a venue");
		}

		var eventEntity = await _eventFinder.GetById(eventId);

		if (eventEntity == null)
		{
			return NotFound();
		}

		if (eventEntity.VenueId != user.VenueId)
		{
			return Forbid();
		}

		return null;
	}
}