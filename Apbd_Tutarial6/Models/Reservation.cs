using System.ComponentModel.DataAnnotations;

namespace Apbd_Tutarial6.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public string OrganizerName { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    public string Status { get; set; }

    public Reservation() { }

    public Reservation(int id, int roomId, string organizerName, string topic,
        DateTime date, TimeSpan startTime, TimeSpan endTime, string status)
    {
        Id = id;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
    }
}