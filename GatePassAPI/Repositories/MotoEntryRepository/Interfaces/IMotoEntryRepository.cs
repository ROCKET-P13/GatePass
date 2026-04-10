using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.MotoEntryRepository.Interfaces;

public interface IMotoEntryRepository
{
	public Task<IEnumerable<MotoEntry>> UpsertMany(IEnumerable<MotoEntry> motoEntries);
}