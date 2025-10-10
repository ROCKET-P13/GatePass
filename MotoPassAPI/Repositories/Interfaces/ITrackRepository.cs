using MotoPassAPI.Entities;

namespace MotoPassAPI.Repositories.Interfaces;

public interface ITrackRepository
{
	public Task<Track> Upsert(Track track);
}