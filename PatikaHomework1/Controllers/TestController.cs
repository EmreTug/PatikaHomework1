using Microsoft.AspNetCore.Mvc;

namespace PatikaHomework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetExceptionHandlingMiddlewareTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                int zero = 0;
                int result = 5 / zero;// burada bir exception oluşacaktır

                return Ok(result);
            }
            catch (Exception ex)
            {
                // oluşan exception, ExceptionHandlingMiddleware tarafından handle edilecektir
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }

}
