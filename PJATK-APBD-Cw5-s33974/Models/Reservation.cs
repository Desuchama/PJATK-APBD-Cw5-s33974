using System.Runtime.InteropServices.JavaScript;
using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.Models;

public class Reservation
{
    private static int _nextId = 1;
    private int Id { get; set; } 
    int RoomId { get; set; }
    string OrganizerName { get; set; } 
    string Topic { get; set; } 
    DateTime Date { get; set; }
    DateTime StartTime { get; set; }
    DateTime EndTime { get; set; }
    Status Status { get; set; }

    public Reservation(int roomId, string organizerName, string topic, DateTime startTime, DateTime endTime,
        string resStatus)
    {
        Id = _nextId;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        StartTime = startTime;
        EndTime = endTime;
        Date = StartTime.Date;
        if (Enum.TryParse<Status>(resStatus, true, out var status))
        {
            Status = status;
        }
        else throw new Exception($"Invalid reservation status {status}");
    }
}