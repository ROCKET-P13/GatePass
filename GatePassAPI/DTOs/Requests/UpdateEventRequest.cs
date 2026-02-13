using GatePassAPI.Enums;

namespace GatePassAPI.DTOs.Requests;

public class UpdateEventRequest
{
	public EventStatus Status { get; set; }
}