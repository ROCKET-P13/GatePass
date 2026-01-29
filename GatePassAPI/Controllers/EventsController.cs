using GatePassAPI.DTOs.Requests;
using GatePassAPI.Factories.EventFactory.DTOs;
using GatePassAPI.Factories.EventFactory.Interfaces;
using GatePassAPI.Finders.EventFinder.Interfaces;
using GatePassAPI.Repositories.EventRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[Route("api/[controller]")]
public class EventsController
(
	IEventFinder eventFinder,
	IEventFactory eventFactory,
	IEventRepository eventRepository
) : ControllerBase
{
	private readonly IEventFinder _eventFinder = eventFinder;
	private readonly IEventFactory _eventFactory = eventFactory;
	private readonly IEventRepository _eventRepository = eventRepository;

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var events = await _eventFinder.GetAll();
		return Ok(events);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddEventRequest request)
	{
		var eventEntity = _eventFactory.FromDto(
			new EventFactoryDTO
			{
				VenueId = request.VenueId,
				Name = request.Name,
				Date = request.Date,
				Status = request.Status,
				ParticipantCapacity = request.ParticipantCapacity
			}
		);

		var newEvent = await _eventRepository.Upsert(eventEntity);

		return Ok(newEvent);

	}
}