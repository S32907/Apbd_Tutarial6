using System.ComponentModel.DataAnnotations;

namespace Apbd_Tutarial6.Models;

public class Room
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Required]
    public string BuildingCode { get; set; }

    public int Floor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
    
    public Room() { }
    
    public Room(int id, string name, string buildingCode, int floor, int capacity, bool hasProjector, bool isActive)
    {
        Id = id;
        Name = name;
        BuildingCode = buildingCode;
        Floor = floor;
        Capacity = capacity;
        HasProjector = hasProjector;
        IsActive = isActive;
    }
}