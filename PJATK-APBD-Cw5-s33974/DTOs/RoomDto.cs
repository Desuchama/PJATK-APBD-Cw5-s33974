using System.ComponentModel.DataAnnotations;
using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string BuildingCode { get; set; } = string.Empty;
    public int Floor { get; set; }
    [MinLength(1)]
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}