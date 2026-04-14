using System.ComponentModel.DataAnnotations;

namespace Apbd_Tutarial6.DTOs;

public class CreatRoomDto
{
    [StringLength(20)]
    public string Name { get; set; }
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
}