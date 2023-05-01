using Application.DTOs.ApiResponse;
using Application.Services.Skills.Commands;
using Application.Services.Skills.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Proyect>>>> Get(int UserId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllSkillsQuery(UserId), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] CreateSkillCommand createSkillCommand,CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createSkillCommand, cancellationToken));
        }

    }
}
