using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController
{
    private static List<Reservation> _reservations =
    [
        new Reservation(
            1,
            "prowadzacy1",
            "AM",
            new DateTime(2026, 4, 16, 8, 30, 0),
            new DateTime(2026, 4, 16, 10, 0, 0),
            "Planned"
        ),
        new Reservation(
            1,
            "prowadzacy1",
            "ALG",
            new DateTime(2026, 4, 16, 10, 15, 0),
            new DateTime(2026, 4, 16, 11, 45, 0),
            "Planned"
        ),
        new Reservation(
            2,
            "prowadzacy2",
            "GUI",
            new DateTime(2026, 4, 16, 8, 30, 0),
            new DateTime(2026, 4, 16, 10, 0, 0),
            "Confirmed"
        ),
        new Reservation(
            3,
            "prowadzacy3",
            "NAI",
            new DateTime(2026, 4, 16, 14, 00, 0),
            new DateTime(2026, 4, 16, 15, 30, 0),
            "Cancelled"
        )
    ];
}