using MotoPassAPI.Entities;

namespace MotoPassAPI.Finders.TrackFinder.Interfaces;

public interface ITrackFinder
{
	Task<List<Track>> GetAll();
}