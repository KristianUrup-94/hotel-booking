using Bookings.Entity;
using Bookings.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Config;
using Shared.Interfaces;
using Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ISimpleService<Booking> _bookingService;
        private readonly IBookingManager _bookingManager;

        public BookingsController(ISimpleService<Booking> bookingService, IBookingManager bookingManager) 
        {
            _bookingService = bookingService;
            _bookingManager = bookingManager;
        }
        
        /// <summary>
        /// Endpoint getting all of the bookings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BookingDTO>> Get()
        {
            try
            {
                return Ok(_bookingService.GetAll().Select(x => MapperConfig.Automapper<Booking, BookingDTO>(x)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for getting a booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BookingDTO> Get(int id)
        {
            try
            {
                return Ok(MapperConfig.Automapper<Booking, BookingDTO>(_bookingService.Get(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint for creating a booking
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] BookingDTO dto)
        {
            try
            {
                _bookingService.Create(MapperConfig.Automapper<BookingDTO, Booking>(dto));
                return Ok("Create was successful completed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Endpoint for updating a booking
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] BookingDTO dto)
        {
            try
            {
                _bookingService.Update(MapperConfig.Automapper<BookingDTO, Booking>(dto));
                return Ok("Update was successful completed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for deleting a booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _bookingService.Delete(id);
                return Ok("Deletion was successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for getting roomIds which are booked in the given period
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetAllBookedRoomIds")]
        public ActionResult<List<int>> GetAllBookedRoomIds(AvailableRoomsRequest request)
        {
            try
            {
                return Ok(_bookingManager.GetRoomIdsBookedInPeriod(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
