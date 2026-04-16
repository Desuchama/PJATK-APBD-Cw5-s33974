using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.Models;

public class Room
{
    //private static int _nextId = 1;
    public int Id { get; set; }
    public string Name { get; set; }
    public BuildingCode BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
    
    public string GetCode()
    {
        return BuildingCode.ToString();
    }
}