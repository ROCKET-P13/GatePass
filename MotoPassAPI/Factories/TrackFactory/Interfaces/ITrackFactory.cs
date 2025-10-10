using MotoPassAPI.Entities;
using MotoPassAPI.Factories.TrackFactory.DTOs;

namespace MotoPassAPI.Factories.TrackFactory.Interfaces;

public interface ITrackFactory
{
	public Track FromDto(TrackFactoryDTO dto);
}