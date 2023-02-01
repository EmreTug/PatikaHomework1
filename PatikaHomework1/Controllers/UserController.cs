using Microsoft.AspNetCore.Mvc;
using PatikaHomework1.Attributes;
using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

namespace PatikaHomework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Implementation of a GET endpoint
            // ...
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            // Implementation of a POST endpoint
            // ...
            return Ok();
        }
    }

}
