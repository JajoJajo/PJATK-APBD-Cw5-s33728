using System.ComponentModel.DataAnnotations;

namespace APBD5Controllers.DTOs;

public class RoomDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "BuildingCode is required.")]
    public string BuildingCode { get; set; } = string.Empty;
    
    [Required]
    public int Floor { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater then zero.")]
    public int Capacity { get; set; }
    
    [Required]
    public bool HasProjector { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
}