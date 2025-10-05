using Bookings.Entity;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ISimpleService<Booking> _roomService;

        public BookingsController(ISimpleService<Booking> roomService) 
        {
            _roomService = roomService;
        }
        // GET: api/<RoomController>
        [HttpGet]
        public ActionResult<List<Booking>> Get()
        {
            return new List<Booking> {
                new Booking
                {
                    Id = 1,
                    BookingId = 100001,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 4
                },
                new Booking
                {
                    Id = 2,
                    BookingId = 100002,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 1
                },
                new Booking
                {
                    Id = 3,
                    BookingId = 100003,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 2
                },
                new Booking
                {
                    Id = 4,
                    BookingId = 100004,
                    Comments = "I want some champagne on the bed, together with flowers",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
                new Booking
                {
                    Id = 5,
                    BookingId = 100005,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 7
                },
                new Booking
                {
                    Id = 6,
                    BookingId = 100006,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 10, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 14, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
            };
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
