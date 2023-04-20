using Application.DTOs.ApiResponse;
using Application.DTOS.AuthResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth.Commands
{
    public record AuthUserCommand(string Email, string Password) : IRequest<ApiResponse<AuthenticationResult>>;
}
