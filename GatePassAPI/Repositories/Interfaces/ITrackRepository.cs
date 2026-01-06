using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.Interfaces;

public interface ITrackRepository
{
	public Task<Track> Upsert(Track track);
}