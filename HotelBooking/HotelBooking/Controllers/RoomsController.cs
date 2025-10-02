using Microsoft.AspNetCore.Mvc;
using Rooms.Entity;
using Shared.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rooms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ISimpleService<Room> _roomService;

        public RoomsController(ISimpleService<Room> roomService) 
        {
            _roomService = roomService;
        }
        // GET: api/<RoomController>
        [HttpGet]
        public ActionResult<List<Room>> Get()
        {
            return new List<Room> { 
                new Room 
                { 
                    Id = 1,
                    Name = "Room 101",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room 
                {
                    Id = 2,
                    Name = "Room 102",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room 
                {
                    Id = 3,
                    Name = "Room 103",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room 
                {
                    Id = 4,
                    Name = "Room 201",
                    Description = "This is mini suite, with a  queen size bed"
                },
                new Room
                {
                    Id = 5,
                    Name = "Room 304",
                    Description = "This is suite, with a king size bed and a lovely view over Esbjerg City"
                },
                new Room
                {
                    Id = 6,
                    Name = "Room 305",
                    Description = "This is suite, with a king size bed and a lovely view over Esbjerg City"
                },
                new Room
                {
                    Id = 7,
                    Name = "Room 306",
                    Description = "This is double suite, with a king size bed, a queen size bed in separated rooms and a lovely view over Esbjerg City"
                },

                new Room
                {
                    Id = 8,
                    Name = "Room 701",
                    Description = "This is presidential suite, with a king size bed, a queen size bed in separated rooms and a lovely view over Esbjerg City"
                }
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
