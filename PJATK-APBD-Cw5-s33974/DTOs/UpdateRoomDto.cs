using System.ComponentModel.DataAnnotations;
using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.DTOs;

public class UpdateRoomDto : IValidatableObject
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string BuildingCode { get; set; } = string.Empty;
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Capacity < 1)
            yield return new ValidationResult("Capacity must be greater than zero");

    }
}