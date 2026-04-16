using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.DTOs;
using PJATK_APBD_Cw5_s33974.Enums;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    public static List<Reservation> _reservations =
    [
        new Reservation()
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "prowadzacy1",
            Topic = "AM",
            Date = new DateOnly(2026, 4, 16),
            StartTime = new TimeSpan( 8, 30, 0),
            EndTime = new TimeSpan( 10, 0, 0),
            Status = Status.Planned
        },
        new Reservation(){
            Id = 2,
            RoomId = 1,
            OrganizerName = "prowadzacy1",
            Topic = "ALG",
            Date = new DateOnly(2026, 4, 16),
            StartTime = new TimeSpan( 10, 15, 0),
            EndTime = new TimeSpan( 11, 45, 0),
            Status = Status.Planned
        },
        new Reservation()
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "prowadzacy2",
            Topic = "GUI",
            Date = new DateOnly(2026, 4, 16),
            StartTime = new TimeSpan(8, 30, 0),
            EndTime = new TimeSpan(10, 0, 0),
            Status = Status.Confirmed
        },
        new Reservation()
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "prowadzacy3",
            Topic = "NAI",
            Date = new DateOnly(2026, 4, 16),
            StartTime = new TimeSpan(14, 00, 0),
            EndTime = new TimeSpan(15, 30, 0),
            Status = Status.Cancelled
        }
    ];
    
    [HttpGet]
    public IActionResult Get(DateOnly? date, string? status, int? roomId)
    {   var reservation = _reservations
            .Where(r => r.Date == date || date is null)
            .Where(r => r.Status.ToString() == status  || status is null)
            .Where(r => r.RoomId == roomId || roomId is null)
            .Select(r => new ReservationDto()
            {
                Id = r.Id,
                RoomId = r.RoomId,
                OrganizerName = r.OrganizerName,
                Topic =  r.Topic,
                Date = r.Date,
                StartTime = r.StartTime,
                EndTime = r.StartTime,
                Status = r.GetStatus()
            });
        if (reservation.LastOrDefault() == null)
        {
            return NotFound("No reservations with matching parameters.");
        }
        else return Ok(reservation);
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
            EndTime = reservation.EndTime,
            Status = reservation.GetStatus()
        });
    }
    [HttpPost]
    public IActionResult Post([FromBody] UpdateReservationDto cRes)
    {
        if (!Enum.TryParse<Status>(cRes.Status, true, out var parsed))
        {
            return BadRequest($"Invalid Status: {cRes.Status}");
        }
        if (!CheckAvailable(cRes)) return 
            BadRequest($"Room unavailable");

        var reservation = new Reservation()
        {
            Id = _reservations.Max(r => r.Id + 1),
            RoomId = cRes.RoomId,
            OrganizerName = cRes.OrganizerName,
            Topic = cRes.Topic,
            Date = cRes.Date,
            StartTime = cRes.StartTime,
            EndTime = cRes.StartTime,
            Status = parsed
        };
        _reservations.Add(reservation);
        return CreatedAtAction(nameof(GetById), new {id = reservation.Id}, reservation);
    }
    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] UpdateReservationDto uRes) {
        var reservation = _reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Reservation with id: {id} does not exist");
        }
        // if (!CheckAvailable(uRes)) return 
        //     BadRequest($"Room unavailable");
        
        else if (Enum.TryParse<Status>(uRes.Status, true, out var parsed))
        {
            reservation.RoomId = uRes.RoomId;
            reservation.OrganizerName = uRes.OrganizerName;
            reservation.Topic = uRes.Topic;
            reservation.Date = uRes.Date;
            reservation.StartTime = uRes.StartTime;
            reservation.EndTime = uRes.StartTime;
            reservation.Status = parsed;
        }
        else throw new ArgumentException($"Invalid Status: {uRes.Status}");
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Room with id: {id} does not exist");
        }
        _reservations.Remove(reservation);
        return NoContent();
    }

    private bool CheckAvailable(UpdateReservationDto uRes)
    {
        var roomExists = RoomsController._rooms
            .Where(r => r.Id == uRes.RoomId)
            .FirstOrDefault(r => r.IsActive);
        var reservationOverlap = _reservations
            .Where(r => r.Date == uRes.Date)
            .Where(r => r.RoomId == uRes.RoomId)
            .Where(r => r.Status != Status.Cancelled)
            .FirstOrDefault(r => r.EndTime >= uRes.StartTime && r.StartTime <= uRes.EndTime ||
                                 uRes.EndTime >= r.StartTime && uRes.StartTime <= r.EndTime);
        return (roomExists != null) && (reservationOverlap == null);
    }
}