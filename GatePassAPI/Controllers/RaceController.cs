using GatePassAPI.Controllers.DTOs;
using GatePassAPI.Factories.MotoFactory.DTOs;
using GatePassAPI.Factories.MotoFactory.Interfaces;
using GatePassAPI.Finders.MotoFinder.Interfaces;
using GatePassAPI.Repositories.MotoEntryRepository.Interfaces;
using GatePassAPI.Repositories.MotoRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatePassAPI.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/race")]
[Authorize]
public class RaceController
(
	IMotoFactory motoFactory,
	IMotoRepository motoRepository,
	IMotoEntryRepository motoEntryRepository,
	IMotoFinder motoFinder

) : ControllerBase
{
	private readonly IMotoFactory _motoFactory = motoFactory;
	private readonly IMotoRepository _motoRepository = motoRepository;
	private readonly IMotoEntryRepository _motoEntryRepository = motoEntryRepository;
	private readonly IMotoFinder _motoFinder = motoFinder;

	[HttpPost("classes/{eventClassId:guid}/motos/generate")]
	public async Task<IActionResult> GenerateMotos(Guid eventId, Guid eventClassId,[FromBody] GenerateMotosRequest request)
	{
		var existingMotos = await _motoFinder.GetByEventClassId(eventClassId);

		if (existingMotos.Count != 0)
		{
			return BadRequest("Motos already generated");
		}

		var result = await _motoFactory.FromDto(
			new MotoFactoryDTO
			{
				EventId = eventId,
				EventClassId = eventClassId,
				GateSize = request.GateSize
			}
		);

		if (result.Motos.Count == 0)
		{
			return Ok();
		}

		await _motoRepository.UpsertMany(result.Motos);
		await _motoEntryRepository.UpsertMany(result.MotoEntries);

		return Ok(result);
	}

	[HttpGet("classes/{eventClassId:guid}/motos")]
	public async Task<IActionResult> GetMotosForClass(Guid eventClassId)
	{
		var existingMotos = await _motoFinder.GetByEventClassId(eventClassId);
		return Ok(existingMotos);
	}
}