using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.VenueRepository.Interfaces;

namespace GatePassAPI.Repositories.VenueRepository;

public class VenueRepository(AppDatabaseContext databaseContext) : IVenueRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Venue> Upsert(Venue venue)
	{
		_databaseContext.Venues.Add(venue);
		await _databaseContext.SaveChangesAsync();
		return venue;
	}
}