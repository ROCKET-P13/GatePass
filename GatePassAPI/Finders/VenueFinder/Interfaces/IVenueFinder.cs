using GatePassAPI.Entities;

namespace GatePassAPI.Finders.VenueFinder.Interfaces;

public interface IVenueFinder
{
	Task<List<Venue>> GetAll();
}