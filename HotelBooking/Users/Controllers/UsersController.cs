using Microsoft.AspNetCore.Mvc;
using Shared.Config;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;
using Shared.Models;
using Users.Entity;
using Users.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISimpleService<User> _userService;

        public UsersController(ISimpleService<User> userService) 
        {
            _userService = userService;
        }
        /// <summary>
        /// Endpoint for getting a list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<UserDTO>> Get()
        {
            try
            {
                return Ok(_userService.GetAll().Select(x => MapperConfig.Automapper<User, UserDTO>(x)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for getting a single user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            try
            {
                var user = _userService.Get(id);
                if (user is null)
                {
                    return NoContent();
                }
                return Ok(MapperConfig.Automapper<User,UserDTO>(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Endpoint for creating a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            try
            {
                _userService.Create(MapperConfig.Automapper<UserDTO, User>(user));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for updating a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserDTO user)
        {
            try
            {
                _userService.Update(MapperConfig.Automapper<UserDTO, User>(user));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for deleting a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
