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
    public class ProyectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProyectsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Proyect>>>> Get(int UserId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProyectsQuery(UserId), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] CreateProyectCommand CreateProyectCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(CreateProyectCommand, cancellationToken);
            return response.ErrorDetail is null ? Ok(response) : StatusCode(response.ErrorDetail.StatusCode, response);
        }
    }
}
