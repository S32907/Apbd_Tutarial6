using Apbd_Tutarial6.DTOs;
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
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? activeOnly)
        {
            var rooms = _rooms.AsQueryable();

            if (minCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly.HasValue)
                rooms = rooms.Where(r => r.IsActive == activeOnly.Value);

            if (!rooms.Any())
                return NotFound();

            return Ok(rooms);
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
        
        // POST /api/rooms
        [HttpPost]
        public IActionResult CreateRoom([FromBody] CreatRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int newId = _rooms.Max(r => r.Id) + 1;

            var room = new Room(
                newId,
                dto.Name,
                dto.BuildingCode,
                dto.Floor,
                dto.Capacity,
                false,  
                true     
            );

            _rooms.Add(room);

            return Created($"api/rooms/{room.Id}", room);
        }

        // PUT api/rooms/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return NoContent();
        }

        //DELETE api/rooms/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            _rooms.Remove(room);

            return NoContent();
        }
    }
}
        
