using Microsoft.AspNetCore.Mvc;

namespace Tech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        public TestController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("asd");
        }
    }
}
