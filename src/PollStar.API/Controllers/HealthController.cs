using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PollStar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                CacheEndpoint = Environment.GetEnvironmentVariable("Cache_Endpoint"),
                AzureStorageAccount = Environment.GetEnvironmentVariable("Azure_StorageAccount"),
            });
        }
    }
}
