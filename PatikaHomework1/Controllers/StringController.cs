using Microsoft.AspNetCore.Mvc;
using PatikaHomework1.Extensions;

namespace PatikaHomework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        [HttpGet("GetContainsWord/{text}/{word}")]
        public IActionResult GetContainsWord(string text, string word)
        {
            return Ok(text.ContainsWord(word));
        }
    }
}
