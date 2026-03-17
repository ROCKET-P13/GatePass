using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Finders.ParticipantFinder.DTOs;
using GatePassAPI.Finders.ParticipantFinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePassAPI.Finders.ParticipantFinder;

public class ParticipantFinder(AppDatabaseContext databaseContext) : IParticipantFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<List<ParticipantViewModel>> GetAll(Guid venueId)
	{
		var participants = await _databaseContext.Participants
			.Where(p => p.VenueId == venueId)
			.Select(p => new ParticipantViewModel
			{
				Id = p.Id,
				FirstName = p.FirstName,
				LastName = p.LastName,
				CreatedAt = p.CreatedAt
			})
			.ToListAsync();

		return participants; 
	}

	public async Task<Participant?> GetById (Guid participantId)
	{
		return await _databaseContext.Participants
			.FirstOrDefaultAsync(p => p.Id == participantId);
	}
}