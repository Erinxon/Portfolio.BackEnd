using Application.DTOs.ApiResponse;
using Application.Services.Languages.Queries;
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
    public class LanguagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguagesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ViewLanguage>>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetLanguagesQuery(), cancellationToken));
        }
    }
}
