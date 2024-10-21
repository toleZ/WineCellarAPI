using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WineCellarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        [Route("User")]
        [Authorize]
        public IActionResult CreateUser([FromBody] CreateUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("The request's body is null.");
            }
            try
            {
                _userServices.CreateUser(userDTO);
            }
            catch (InvalidOperationException)
            {
                return BadRequest($"A user with the username {userDTO.Username.ToUpper()} already exists and can't store duplicates");
            }
            return Created("Location", "Resource");
        }
    }
}