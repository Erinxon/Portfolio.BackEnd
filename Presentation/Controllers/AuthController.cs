using Application.Auth.Commands;
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
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var authResult = await this._mediator.Send(createUserCommand, cancellationToken);

            return authResult is not null ? Created("/Auth", authResult) : BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthUserCommand authUserCommand, CancellationToken cancellationToken)
        {
            var authResult = await this._mediator.Send(authUserCommand, cancellationToken);

            return authResult is not null ? Ok(authResult) : Unauthorized();
        }
    }
}
