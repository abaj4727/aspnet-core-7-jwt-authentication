using JWTImplimentation.Interfaces;
using JWTImplimentation.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTImplimentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService employeeService)
        {
            _authService = employeeService;
        }
        // POST api/<AuthController>
        [HttpPost("login")]
        public string Login([FromBody] LoginRequest value)
        {
            var rsult= _authService.Login(value);
            return rsult;
        }
        // POST api/<AuthController>
        [HttpPost("adduser")]
        public User AddUser([FromBody] User value)
        {
            var user= _authService.AddUser(value);  
            return user;
        }
    }
}
