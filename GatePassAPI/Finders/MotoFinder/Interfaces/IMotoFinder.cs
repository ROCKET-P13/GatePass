using GatePassAPI.Finders.MotoFinder.DTOs;

namespace GatePassAPI.Finders.MotoFinder.Interfaces;

public interface IMotoFinder
{
	Task<List<MotoViewModel>> GetByEventClassId(Guid eventClassId);
}