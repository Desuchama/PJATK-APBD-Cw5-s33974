using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.Models;

public class Room
{
    private static int _nextId = 1;
    int Id { get; set; }
    string Name { get; set; }
    BuildingCode BuildingCode { get; set; } 
    int Floor { get; set; }
    int Capacity { get; set; }
    bool HasProjector { get; set; }
    bool IsActive { get; set; }
    
    public Room (string name, char buildingCode, int floor, int capacity, bool hasProjector, bool isActive) 
    {
        Id = _nextId++;
        Name = name;
        Floor = floor;
        Capacity = capacity;
        HasProjector = hasProjector;
        IsActive = isActive;

        if (Enum.TryParse<BuildingCode>(buildingCode.ToString(), true, out var code))
        {
            BuildingCode = code;
        }
        else throw new Exception($"Invalid building code {buildingCode}");
    }
}