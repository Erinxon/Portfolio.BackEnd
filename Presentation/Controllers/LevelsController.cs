using Application.DTOs.ApiResponse;
using Application.Services.Levels.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LevelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LevelsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ViewLevel>>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetLevelsQuery(), cancellationToken));
        }

    }
}
