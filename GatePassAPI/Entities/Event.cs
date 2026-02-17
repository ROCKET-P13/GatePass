using GatePassAPI.Enums;

namespace GatePassAPI.Entities;

public class Event
{
	public Guid Id { get; set; }
	public Guid VenueId { get; set; }
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public required EventStatus Status { get; set; }
	public int? ParticipantCapacity { get; set; }

	public void Update(
		string? name,
		DateTimeOffset? startDateTime,
		EventStatus? status,
		int? participantCapacity
	)
	{
		if (!string.IsNullOrWhiteSpace(name))
		{
			Name = name;
		}

		if (startDateTime.HasValue)
		{
			StartDateTime = startDateTime.Value;
		}

		if (status.HasValue)
		{
			Status = status.Value;
		}

		if (participantCapacity.HasValue)
		{
			ParticipantCapacity = participantCapacity.Value;
		}
	}
}