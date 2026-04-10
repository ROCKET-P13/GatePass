using GatePassAPI.Entities;
using GatePassAPI.Enums;
using GatePassAPI.Factories.MotoFactory.DTOs;
using GatePassAPI.Factories.MotoFactory.Interfaces;
using GatePassAPI.Finders.EventClassFinder.Interfaces;
using GatePassAPI.Finders.EventRegistrationFinder.DTOs;
using GatePassAPI.Finders.EventRegistrationFinder.Interfaces;

namespace GatePassAPI.Factories.MotoFactory;

public class MotoFactory
(
	IEventClassFinder eventClassFinder,
	IEventRegistrationFinder eventRegistrationFinder
) : IMotoFactory
{

	private readonly IEventClassFinder _eventClassFinder = eventClassFinder;
	private readonly IEventRegistrationFinder _eventRegistrationFinder = eventRegistrationFinder;

	private const int DefaultGateSize = 20;

	public async Task<MotoFactoryFromDTOsResult> FromDto(MotoFactoryDTO dto)
	{
		var eventClass = await _eventClassFinder.GetById(dto.EventClassId) ?? throw new InvalidOperationException("EventClass does not exist");

		if (eventClass.EventId != dto.EventId)
		{
			throw new InvalidOperationException("EventClass does not belong to Event");
		}

		var registrations = await _eventRegistrationFinder.GetByEventClassId(dto.EventClassId);

		if (registrations.Count == 0)
		{
			return new MotoFactoryFromDTOsResult();
		}

		var gate = dto.GateSize ?? DefaultGateSize;

		var shuffled = registrations
			.OrderBy(_ => Guid.NewGuid())
			.ToList();

		var motos = CreateMotos(dto.EventClassId, shuffled.Count, gate);
		var motoEntries = CreateMotoEntries(motos, registrations, gate);

		return new MotoFactoryFromDTOsResult
		{
			Motos = motos,
			MotoEntries = motoEntries
		};

	}

	private static List<Moto> CreateMotos(Guid eventClassId, int riderCount, int gateSize)
	{
		var motoCount = (int)Math.Ceiling(riderCount / (double)gateSize);

		var motos = new List<Moto>();

		for (int i = 0; i < motoCount; i++)
		{
			motos.Add(
				new Moto
				{
					Id = Guid.NewGuid(),
					EventClassId = eventClassId,
					MotoNumber = i + 1,
					Type = MotoType.Heat,
					Status = MotoStatus.Scheduled,
					CreatedAt = DateTime.UtcNow
				}
			);
		}

		return motos;
	}

	private static List<MotoEntry> CreateMotoEntries(List<Moto> motos, List<ParticipantRegistrationViewModel> eventRegistrations, int gateSize)
	{
		var motoEntries = new List<MotoEntry>();

		for (int i = 0; i < motos.Count; i++)
		{
			var moto = motos[i];

			var registrations = eventRegistrations
				.Skip(i * gateSize)
				.Take(gateSize)
				.ToList();

			int gatePick = 1;

			foreach(var registration in registrations)
			{
				motoEntries.Add(
					new MotoEntry
					{
						Id = Guid.NewGuid(),
						MotoId = moto.Id,
						EventRegistrationId = registration.Id,
						GatePick = gatePick++,
						CreatedAt = DateTime.UtcNow
					}
				);
			}
		}

		return motoEntries;
	}
}