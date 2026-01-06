namespace MotoPassAPI.DTOs.Requests;

public class AddTrackRequest
{
	public required string Name { get; set; }
	public string LogoImageURL { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public required string AddressLine1 { get; set; }
	public string AddressLine2 { get; set; } = string.Empty;
	public required string City { get; set; }
	public required string State { get; set; }
	public required string Country { get; set; }
}