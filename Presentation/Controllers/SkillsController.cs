using Application.Skills.Commands;
using Application.Skills.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllSkillsQuery(), cancellationToken));
        }

        [HttpGet("{SkillId}")]
        public async Task<ActionResult> GetById(int SkillId, CancellationToken cancellationToken)
        {
            var skill = await _mediator.Send(new GetByIdSkillsQuery(SkillId), cancellationToken);
            if(skill is null)
            {
                return NotFound("Skill NotFound");
            }
            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSkillCommand createSkillCommand,CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createSkillCommand, cancellationToken));
        }

    }
}
