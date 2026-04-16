using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.DTOs;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static List<Room> _rooms =
    [
        new Room(
            "B108",
            "B",
            1,
            16,
            true,
            true
        ),
        new Room("B240a",
            "B",
            2,
            18,
            false,
            true
        ),
        new Room(
            "A360",
            "A",
            3,
            20,
            true,
            true),
        new Room(
            "C100",
            "C",
            0,
            0,
            false,
            false),
    ];
    [HttpGet]
    public IActionResult GetAll()
    {   
        return Ok(_rooms.Select(r => new RoomDto()
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
}