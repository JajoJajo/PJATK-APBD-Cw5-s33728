using System.ComponentModel.DataAnnotations;

namespace APBD5Controllers.DTOs;

public class ReservationDto
{
    [Required]
    public int RoomId { get; set; }
 
    [Required(ErrorMessage = "OrganizerName is required.")]
    public string OrganizerName { get; set; } = string.Empty;
 
    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public TimeOnly StartTime { get; set; }
    
    [Required]
    public TimeOnly EndTime { get; set; }
 
    [Required]
    public string Status { get; set; } = "planned";
}