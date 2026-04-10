using GatePassAPI.Entities;

namespace GatePassAPI.Factories.MotoFactory.DTOs;

public class MotoFactoryFromDTOsResult
{
	public List<Moto> Motos { get; set; } = [];
	public List<MotoEntry> MotoEntries { get; set; } = [];
}