using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.Interfaces;

namespace GatePassAPI.Repositories.TrackRepository;

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