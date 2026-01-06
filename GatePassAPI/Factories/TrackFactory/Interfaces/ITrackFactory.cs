using GatePassAPI.Entities;
using GatePassAPI.Factories.TrackFactory.DTOs;

namespace GatePassAPI.Factories.TrackFactory.Interfaces;

public interface ITrackFactory
{
	public Track FromDto(TrackFactoryDTO dto);
}