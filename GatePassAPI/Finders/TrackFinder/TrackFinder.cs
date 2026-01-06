using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.TrackFinder.Interfaces;

namespace GatePassAPI.Finders.TrackFinder;

public class TrackFinder(AppDatabaseContext databaseContext) : ITrackFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<Track>> GetAll()
	{
		return await _databaseContext.Tracks.ToListAsync();
	}
}