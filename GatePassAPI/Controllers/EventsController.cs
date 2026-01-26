using GatePassAPI.Finders.EventFinder.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[Route("api/[controller]")]
public class EventsController
(
	IEventFinder eventFinder
) : ControllerBase
{
	private readonly IEventFinder _eventFinder = eventFinder;

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var events = await _eventFinder.GetAll();
		return Ok(events);
	}
}