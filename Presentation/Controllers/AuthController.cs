using Application.DTOs.ApiResponse;
using Application.DTOS.AuthResult;
using Application.Services.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<AuthenticationResult>>> Post([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var authResult = await this._mediator.Send(createUserCommand, cancellationToken);

            return authResult is not null ? Created("/Auth", authResult) : BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<AuthenticationResult>>> Login([FromBody] AuthUserCommand authUserCommand, CancellationToken cancellationToken)
        {
            var authResult = await this._mediator.Send(authUserCommand, cancellationToken);

            return authResult is not null ? Ok(authResult) : Unauthorized();
        }
    }
}
