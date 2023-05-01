using Application.DTOs.ApiResponse;
using Application.Services.WorkExperience.Commands;
using Application.Services.WorkExperience.Queries;
using Domain.Entities;
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
    public class WorkExperienceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkExperienceController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ViewWorkExperience>>>> Get(int UserId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetWorkExperienceQuery(UserId), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] CreateWorkExperienceCommand CreateWorkExperienceCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(CreateWorkExperienceCommand, cancellationToken));
        }
    }
}
