namespace GatePassAPI.Entities;

public class User
{
	public required Guid Id { get; set; }
	public required string Auth0Id { get; set; }
	public Guid? VenueId { get; private set; }
	public required string Email { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required DateTime CreatedAt { get; set; }

	public void AssignToVenue(Guid venueId)
	{
		if (venueId == Guid.Empty)
		{
			throw new ArgumentException("venueId is required");
		}

		VenueId = venueId;
	}

	public void RemoveFromVenue()
	{
		VenueId = null;
	}
}