using Microsoft.AspNetCore.Mvc;
using PollStar.Polls.Abstractions.Services;
using PollStar.Sessions.Abstractions.DataTransferObjects;
using PollStar.Sessions.Abstractions.Services;
using PollStar.Sessions.ErrorCodes;
using PollStar.Sessions.Exceptions;

namespace PollStar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly IPollStarSessionsService _service;
        private readonly IPollStarPollsService _pollsService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery] Guid userId)
        {
            try
            {
                var service = await _service.GetSessionAsync(id, userId);
                return Ok(service);
            }
            catch (PollStarSessionException psEx)
            {
                if (psEx.ErrorCode == PollStarSessionErrorCode.SessionNotFound)
                {
                    return new NotFoundResult();
                }
            }

            return BadRequest();
        }

        [HttpGet("{id}/polls")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pollsList = await _pollsService.GetPollsListAsync(id);
                return Ok(pollsList);
            }
            catch (PollStarSessionException psEx)
            {
                if (psEx.ErrorCode == PollStarSessionErrorCode.SessionNotFound)
                {
                    return new NotFoundResult();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSessionDto dto)
        {
                var createdService = await _service.CreateSessionAsync(dto);
                return Ok(createdService);
        }



        public SessionsController(IPollStarSessionsService service, IPollStarPollsService pollsService)
        {
            _service = service;
            _pollsService = pollsService;
        }

    }
}
