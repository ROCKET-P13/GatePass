using MotoPassAPI.Data;
using MotoPassAPI.Entities;
using MotoPassAPI.Repositories.Interfaces;

namespace MotoPassAPI.Repositories.TrackRepository;

public class TrackRepository(AppDatabaseContext databaseContext) : ITrackRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Track> Upsert(Track track)
	{
		_databaseContext.Tracks.Add(track);
		await _databaseContext.SaveChangesAsync();
		return track;
	}
}