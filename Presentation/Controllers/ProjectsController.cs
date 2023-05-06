using Application.DTOs.ApiResponse;
using Application.Services.Proyects.Commands;
using Application.Services.Proyects.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Proyect>>>> Get(int UserId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProjectsQuery(UserId), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] CreateProjectCommand CreateProjectCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(CreateProjectCommand, cancellationToken);
            return response.ErrorDetail is null ? Ok(response) : StatusCode(response.ErrorDetail.StatusCode, response);
        }
    }
}
