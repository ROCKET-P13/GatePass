using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.VenueFinder.Interfaces;

namespace GatePassAPI.Finders.VenueFinder;

public class VenueFinder(AppDatabaseContext databaseContext) : IVenueFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<Venue>> GetAll()
	{
		return await _databaseContext.Venues.ToListAsync();
	}
}