using Microsoft.AspNetCore.Mvc;

namespace react_app.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("/[controller]/test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                Ok = 1
            });
        }
    }
}
