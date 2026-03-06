using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.ParticipantFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.ParticipantFinder;

public class ParticipantFinder(AppDatabaseContext databaseContext) : IParticipantFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<Participant>> GetAll(Guid venueId)
	{
		var participants = await _databaseContext.Participants
			.Where(p =>
			p.VenueId == venueId)
			.ToListAsync();

		return participants; 
	}
}