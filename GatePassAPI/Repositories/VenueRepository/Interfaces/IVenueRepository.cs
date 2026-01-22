using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.VenueRepository.Interfaces;

public interface IVenueRepository
{
	public Task<Venue> Upsert(Venue venue);
}