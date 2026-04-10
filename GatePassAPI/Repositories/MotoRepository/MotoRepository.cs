using GatePassAPI.Data;
using GatePassAPI.Entities;
using GatePassAPI.Repositories.MotoRepository.Interfaces;

namespace GatePassAPI.Repositories.MotoRepository;

public class MotoRepository(AppDatabaseContext databaseContext) : IMotoRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<IEnumerable<Moto>> UpsertMany(IEnumerable<Moto> motos)
	{
		await _databaseContext.Motos.AddRangeAsync(motos);
		await _databaseContext.SaveChangesAsync();
		return motos;
	}
}