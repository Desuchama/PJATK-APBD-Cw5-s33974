using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.DTOs;
using PJATK_APBD_Cw5_s33974.Enums;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static List<Room> _rooms =
    [
        new Room()
        {
            Id = 1,
            Name =  "B108",
            BuildingCode = BuildingCode.B,
            Floor = 1,
            Capacity = 16,
            HasProjector = true,
            IsActive = true
        },
        new Room()
        {
            Id = 2,
            Name = "B240a",
            BuildingCode = BuildingCode.B,
            Floor = 2,
            Capacity = 18,
            HasProjector =  false,
            IsActive = true
        },
        new Room()
        {
            Id = 3,
            Name = "A360",
            BuildingCode = BuildingCode.A,
            Floor = 3,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room()
        {
            Id = 4,
            Name = "C100",
            BuildingCode = BuildingCode.C,
            Floor = 0,
            Capacity = 1,
            HasProjector = false,
            IsActive = false
        }
    ];
    [HttpGet]
    public IActionResult Get(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {   
        return Ok(_rooms
            .Where(r => r.Capacity >= minCapacity || minCapacity is null)
            .Where(r => r.HasProjector == hasProjector  || hasProjector is null)
            .Where(r => r.IsActive == activeOnly || activeOnly is null)
            .Select(r => new RoomDto()
        {
            Id = r.Id,
            Name = r.Name,
            BuildingCode = r.GetCode(),
            Floor = r.Floor,
            Capacity = r.Capacity,
            HasProjector = r.HasProjector,
            IsActive = r.IsActive
        }));
    }
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        
        if (room is null)
        {
            return NotFound($"Room with id: {id} does not exist");
        }
        else return Ok(new RoomDto()
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.GetCode(),
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        });
    }
    [HttpGet("buildings/{buildingId}")]
    public IActionResult GetByBuildingId(string buildingId)
    {   
        if (Enum.TryParse<BuildingCode>(buildingId, false, out var parsed))
            return Ok(_rooms
                .Where(r => buildingId == r.BuildingCode.ToString())
                .Select(r => new RoomDto()
            {
                Id = r.Id,
                Name = r.Name,
                BuildingCode = r.GetCode(),
                Floor = r.Floor,
                Capacity = r.Capacity,
                HasProjector = r.HasProjector,
                IsActive = r.IsActive
            }));
        else return NotFound($"Building with id: {buildingId} does not exist");
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomDto c)
    {
        if (!Enum.TryParse<BuildingCode>(c.BuildingCode, false, out var parsed))
        {
            return BadRequest($"Invalid building code: {c.BuildingCode}");
        }

        var room = new Room()
        {
            Id = _rooms.Max(r => r.Id) + 1,
            Name = c.Name,
            BuildingCode = parsed,
            Floor = c.Floor,
            Capacity = c.Capacity,
            HasProjector = c.HasProjector,
            IsActive = c.IsActive
        };
        Console.WriteLine(room.Id);
        _rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new {id = room.Id}, room);
    }
    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] UpdateRoomDto u) {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        
        if (room is null)
        {
            return NotFound($"Room with id: {id} does not exist");
        }
        else if (Enum.TryParse<BuildingCode>(u.BuildingCode, false, out var parsed))
        {
            room.Name = u.Name;
            room.BuildingCode = parsed;
            room.Floor = u.Floor;
            room.Capacity = u.Capacity;
            room.HasProjector = u.HasProjector;
            room.IsActive = u.IsActive;
        }
        else throw new ArgumentException($"Invalid building code: {u.BuildingCode}");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        
        if (room is null)
        {
            return NotFound($"Room with id: {id} does not exist");
        }
        _rooms.Remove(room);
        return NoContent();
    }
}