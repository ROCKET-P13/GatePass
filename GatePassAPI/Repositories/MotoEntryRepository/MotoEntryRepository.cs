using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.MotoEntryRepository.Interfaces;

namespace GatePassAPI.Repositories.MotoEntryRepository;

public class MotoEntryRepository(AppDatabaseContext databaseContext) : IMotoEntryRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<IEnumerable<MotoEntry>> UpsertMany(IEnumerable<MotoEntry> motoEntries)
	{
		await _databaseContext.AddRangeAsync(motoEntries);
		await _databaseContext.SaveChangesAsync();
		return motoEntries;
	}
}