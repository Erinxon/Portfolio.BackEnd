using Application.DTOs.ApiResponse;
using Application.Services.Platforms.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlatformsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ViewPlatform>>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetPlatformQuery(), cancellationToken));
        }

    }
}
