using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.Interfaces;

public interface IVenueRepository
{
	public Task<Venue> Upsert(Venue venue);
}