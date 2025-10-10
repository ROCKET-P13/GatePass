using Microsoft.EntityFrameworkCore;
using MotoPassAPI.Data;
using MotoPassAPI.Entities;
using MotoPassAPI.Finders.TrackFinder.Interfaces;

namespace MotoPassAPI.Finders.TrackFinder;

public class TrackFinder(AppDatabaseContext databaseContext) : ITrackFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<Track>> GetAll()
	{
		return await _databaseContext.Tracks.ToListAsync();
	}
}