using Microsoft.AspNetCore.Mvc;
using Rooms.Entity;
using Shared.Config;
using Shared.Interfaces;
using Shared.Models;

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
        /// <summary>
        /// Endpoint for getting a list of rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<RoomDTO>> Get()
        {
            try
            {
                return Ok(_roomService.GetAll().Select(x => MapperConfig.Automapper<Room, RoomDTO>(x)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for getting a single room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<RoomDTO> Get(int id)
        {
            try
            {
                var room = _roomService.Get(id);
                if (room is null)
                {
                    return NoContent();
                }
                return Ok(MapperConfig.Automapper<Room,RoomDTO>(room));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Endpoint for creating a room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] RoomDTO room)
        {
            try
            {
                _roomService.Create(MapperConfig.Automapper<RoomDTO,Room>(room));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for updating a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] RoomDTO room)
        {
            try
            {
                _roomService.Update(MapperConfig.Automapper<RoomDTO, Room>(room));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for deleting a room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _roomService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
