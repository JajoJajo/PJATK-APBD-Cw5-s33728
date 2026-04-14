using System.ComponentModel.DataAnnotations;

namespace APBD5Controllers.Models;

public class Reservation
{
    public int Id { get; set; }
 
    public int RoomId { get; set; }
 
    [Required(ErrorMessage = "OrganizerName is required.")]
    public string OrganizerName { get; set; } = string.Empty;
 
    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;
 
    public DateOnly Date { get; set; }
 
    public TimeOnly StartTime { get; set; }
 
    public TimeOnly EndTime { get; set; }
 
    [Required]
    public string Status { get; set; } = "planned";
 
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                [nameof(EndTime)]);
        }
    }
}