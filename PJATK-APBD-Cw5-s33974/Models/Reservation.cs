using System.Runtime.InteropServices.JavaScript;
using PJATK_APBD_Cw5_s33974.Enums;

namespace PJATK_APBD_Cw5_s33974.Models;

public class Reservation
{
    private static int _nextId = 1;
    public int Id { get; set; } 
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } 
    public string Topic { get; set; } 
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Status Status { get; set; }

    public Reservation(int roomId, string organizerName, string topic, DateOnly date, TimeSpan startTime, TimeSpan endTime,
        string resStatus)
    {
        Id = _nextId++;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        if (Enum.TryParse<Status>(resStatus, true, out var status))
        {
            Status = status;
        }
        else throw new Exception($"Invalid reservation status {status}");
    }
    public string GetStatus()
    {
        return Status.ToString();
    }
}