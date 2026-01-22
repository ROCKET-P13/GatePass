namespace GatePassAPI.Entities;

public class Venue
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public required string Sport { get; set; }
	public required string LogoImageURL { get; set; }
	public required string PhoneNumber { get; set; }
	public required string AddressLine1 { get; set; }
	public required string AddressLine2 { get; set; }
	public required string City { get; set; }
	public required string State { get; set; }
	public required string Country { get; set; }
}