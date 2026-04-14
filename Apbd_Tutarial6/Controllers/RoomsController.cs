using Apbd_Tutarial6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apbd_Tutarial6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private static List<Room> _rooms = new List<Room>()
        {
            new Room(0,"Luxury3L","20l",8,4,true, false ),
            new Room(1,"Basic1B","15b",2,2,false, true ),
            new Room(2,"Advanced2A","15b",5,3,true, true )
        };
        
        
        // Get api/rooms
        [HttpGet]
        public IActionResult Get(
            [FromQuery] int? minCapacity = 0,
            [FromQuery] bool? hasProjector = false,
            [FromQuery] bool? activeOnly = false)
        {
            var rooms = _rooms.Where(r => r.Capacity >= minCapacity)
                .Where(r => r.HasProjector && r.IsActive);
            
            
            return Ok(_rooms);
        }
        
        //Get api/rooms/{id}
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
             Room room = _rooms.Where(r => r.Id == id).FirstOrDefault();

             if (room == null)
             {
                 return NotFound();
             }
             
             return Ok(room);
        }
        
        //Get api/rooms/buildingCode/{buildingCode}
        [Route("buildingCode/{buildingCode}")]
        [HttpGet]
        public IActionResult GetRoomsByBuildingCode(string buildingCode)
        {
            List<Room> roomsInBuilding = _rooms.Where(r => r.BuildingCode == buildingCode).ToList();

            if (roomsInBuilding.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(roomsInBuilding);
        }
        
       
    }
}
