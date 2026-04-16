using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw5_s33974.DTOs;

public class UpdateReservationDto : IValidatableObject
{
	public int RoomId { get; set; }
	[Required]
	public string OrganizerName { get; set; } 
	[Required]
	public string Topic { get; set; } 
	public DateOnly Date { get; set; }
	public TimeSpan StartTime { get; set; }
	public TimeSpan EndTime { get; set; }
	public string Status { get; set; }
	
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (EndTime <= StartTime)
		{
			yield return new ValidationResult("Start time can't be after end time");
		}
	}
}