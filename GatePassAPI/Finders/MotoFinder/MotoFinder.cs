using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.MotoFinder.DTOs;
using GatePassAPI.Finders.MotoFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.MotoFinder;

public class MotoFinder(AppDatabaseContext databaseContext) : IMotoFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<MotoViewModel>> GetByEventClassId(Guid eventClassId)
	{
		return await _databaseContext.Motos
		.Where(moto => moto.EventClassId == eventClassId)
		.Select(moto => new MotoViewModel
		{
			Id = moto.Id,
			EventClassId = moto.EventClassId,
			MotoNumber = moto.MotoNumber,
			Type = moto.Type,
			Status = moto.Status,
			StartTime = moto.StartTime,
			CreatedAt = moto.CreatedAt,
		})
		.ToListAsync();
	}
}