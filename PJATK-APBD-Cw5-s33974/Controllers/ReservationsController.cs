using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.DTOs;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private static List<Reservation> _reservations =
    [
        new Reservation(
            1,
            "prowadzacy1",
            "AM",
            new DateOnly(2026, 4, 16),
            new TimeSpan( 8, 30, 0),
            new TimeSpan( 10, 0, 0),
            "Planned"
        ),
        new Reservation(
            1,
            "prowadzacy1",
            "ALG",
            new DateOnly(2026, 4, 16),
            new TimeSpan( 10, 15, 0),
            new TimeSpan( 11, 45, 0),
            "Planned"
        ),
        new Reservation(
            2,
            "prowadzacy2",
            "GUI",
            new DateOnly(2026, 4, 16),
            new TimeSpan(8, 30, 0),
            new TimeSpan(10, 0, 0),
            "Confirmed"
        ),
        new Reservation(
            3,
            "prowadzacy3",
            "NAI",
            new DateOnly(2026, 4, 16),
            new TimeSpan( 14, 00, 0),
            new TimeSpan( 15, 30, 0),
            "Cancelled"
        )
    ];
    
    [HttpGet]
    public IActionResult GetAll()
    {   
        return Ok(_reservations.Select(r => new ReservationDto()
        {
            Id = r.Id,
            RoomId = r.RoomId,
            OrganizerName = r.OrganizerName,
            Topic =  r.Topic,
            Date = r.Date,
            StartTime = r.StartTime,
            EndTime = r.StartTime,
            Status = r.GetStatus()
        }));
    }
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Reservation with id: {id} does not exist");
        }
        else return Ok(new ReservationDto()
        {
            Id = reservation.Id,
            RoomId = reservation.RoomId,
            OrganizerName = reservation.OrganizerName,
            Topic = reservation.Topic,
            Date = reservation.Date,
            StartTime = reservation.StartTime,
            EndTime = reservation.StartTime,
            Status = reservation.GetStatus()
        });
    }
}