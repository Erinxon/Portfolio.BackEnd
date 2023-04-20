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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Skill>>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllSkillsQuery(), cancellationToken));
        }

        [HttpGet("{SkillId}")]
        public async Task<ActionResult<ApiResponse<Skill>>> GetById(int SkillId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdSkillsQuery(SkillId), cancellationToken);
            if(response.Data is null)
            {
                return NotFound("Skill NotFound");
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] CreateSkillCommand createSkillCommand,CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createSkillCommand, cancellationToken));
        }

    }
}
