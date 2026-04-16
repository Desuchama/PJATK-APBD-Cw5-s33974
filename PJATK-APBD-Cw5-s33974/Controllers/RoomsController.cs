using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33974.Models;

namespace PJATK_APBD_Cw5_s33974.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController
{
    private static List<Room> _rooms =
    [
        new Room(
            "B108",
            'B',
            1,
            16,
            true,
            true
        ),
        new Room("B240a",
            'B',
            2,
            18,
            false,
            true
        ),
        new Room(
            "A360",
            'A',
            3,
            20,
            true,
            true),
        new Room(
            "C100",
            'C',
            0,
            0,
            false,
            false)
    ];
}