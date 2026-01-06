using GatePassAPI.Entities;

namespace GatePassAPI.Finders.TrackFinder.Interfaces;

public interface ITrackFinder
{
	Task<List<Track>> GetAll();
}