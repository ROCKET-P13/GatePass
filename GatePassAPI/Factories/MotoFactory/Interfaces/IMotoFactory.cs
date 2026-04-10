using GatePassAPI.Factories.MotoFactory.DTOs;

namespace GatePassAPI.Factories.MotoFactory.Interfaces;

public interface IMotoFactory
{
	public Task<MotoFactoryFromDTOsResult> FromDto(MotoFactoryDTO dto);
}