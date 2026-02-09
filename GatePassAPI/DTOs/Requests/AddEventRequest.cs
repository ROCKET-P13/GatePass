namespace GatePassAPI.DTOs.Requests;

public class AddEventRequest
{
	public required string Name { get; set; }
	public required DateTime Date { get; set; }
	public required string Status { get; set; }
	public int ParticipantCapacity { get; set; }
}