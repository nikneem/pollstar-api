using Microsoft.AspNetCore.Mvc;
using PollStar.Users.Abstractions.Services;

namespace PollStar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPollStarUsersService _service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userDto = await _service.Create();
            return Ok(userDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userDto = await _service.Restore(id);
            return userDto == null ? BadRequest() : Ok(userDto);
        }

        public UsersController(IPollStarUsersService service)
        {
            _service = service;
        }

    }
}
