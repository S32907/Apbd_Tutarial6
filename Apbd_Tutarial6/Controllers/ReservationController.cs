using Apbd_Tutarial6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Apbd_Tutarial6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        
        private static List<Room> _rooms = DataStore.Rooms;

        private static List<Reservation> _reservations = new List<Reservation>()
        {
            new Reservation(1,1,"Alice","C# Basics",DateTime.Today, new TimeSpan(9,0,0), new TimeSpan(10,0,0),"planned"),
            new Reservation(2,2,"Bob","Java Workshop",DateTime.Today, new TimeSpan(10,0,0), new TimeSpan(11,0,0),"confirmed"),
            new Reservation(3,1,"Tom","ASP.NET",DateTime.Today.AddDays(1), new TimeSpan(12,0,0), new TimeSpan(13,0,0),"planned")
        };

        // GET api/reservations
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_reservations);
        }

        // GET api/reservations/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _reservations.FirstOrDefault(r => r.Id == id);

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        // POST api/reservations
        [HttpPost]
        public IActionResult Create([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // EndTime must be later than StartTime
            if (reservation.EndTime <= reservation.StartTime)
                return BadRequest("EndTime must be later than StartTime");

            //Room must exist
            var room = _rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null)
                return NotFound("Room does not exist");
            
            if (!room.IsActive)
                return BadRequest("Room is occupied");

            // Overlap check 
            bool overlap = _reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date.Date == reservation.Date.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime
            );

            if (overlap)
                return Conflict("Reservation overlaps with existing one");

            reservation.Id = _reservations.Max(r => r.Id) + 1;

            _reservations.Add(reservation);

            return Created($"api/reservations/{reservation.Id}", reservation);
        }

        // PUT api/reservations/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reservation updated)
        {
            var existing = _reservations.FirstOrDefault(r => r.Id == id);

            if (existing == null)
                return NotFound();

            if (updated.EndTime <= updated.StartTime)
                return BadRequest("EndTime must be later than StartTime");

            // overlap check (exclude itself)
            bool overlap = _reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updated.RoomId &&
                r.Date.Date == updated.Date.Date &&
                updated.StartTime < r.EndTime &&
                updated.EndTime > r.StartTime
            );

            if (overlap)
                return Conflict();

            existing.RoomId = updated.RoomId;
            existing.OrganizerName = updated.OrganizerName;
            existing.Topic = updated.Topic;
            existing.Date = updated.Date;
            existing.StartTime = updated.StartTime;
            existing.EndTime = updated.EndTime;
            existing.Status = updated.Status;

            return Ok(existing);
        }

        // DELETE api/reservations/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _reservations.FirstOrDefault(r => r.Id == id);

            if (res == null)
                return NotFound();

            _reservations.Remove(res);

            return NoContent();
        }
    }
}