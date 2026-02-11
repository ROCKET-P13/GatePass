namespace GatePassAPI.DTOs.Requests;

public class AddEventRequest
{
	public required string Name { get; set; }
	public DateTimeOffset StartDateTime { get; set; }
	public required string Status { get; set; }
	public int ParticipantCapacity { get; set; }
}