using GatePassAPI.Entities;

namespace GatePassAPI.Repositories.MotoRepository.Interfaces;

public interface IMotoRepository
{
	public Task<IEnumerable<Moto>> UpsertMany(IEnumerable<Moto> motos);
}