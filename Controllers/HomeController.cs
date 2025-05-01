using Microsoft.AspNetCore.Mvc;

namespace Dockerized_DotNetAPI_LoadBalanced.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly string _instanceName;

        public HomeController()
        {
            _instanceName = Environment.GetEnvironmentVariable("API_INSTANCE_NAME") ?? "API";
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = $"Hello from {_instanceName}!",
                timestamp = DateTime.UtcNow,
                status = "Running"
            });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new
            {
                instance = _instanceName,
                uptime = $"{Environment.TickCount64 / 1000} seconds",
                status = "Healthy"
            });
        }
    }
}
