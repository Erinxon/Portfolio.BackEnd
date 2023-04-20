using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.DTOS.AuthResult;
using Application.Services.Auth.Queries;
using Application.Tools;
using Domain.Entities;
using MediatR;

namespace Application.Services.Auth.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApiResponse<AuthenticationResult>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;
        private readonly IMediator mediator;
        private readonly IJwtGenerator jwtGenerator;

        public CreateUserHandler(IFromSqlRawGeneric fromSqlRawGeneric, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
            this.mediator = mediator;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<ApiResponse<AuthenticationResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            int UserId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync($"exec [dbo].[Sp_CreateUser] '{request.Name}', '{request.Email}', '{request.Password.ToEncryptedPassword()}', @Identity out", cancellationToken);

            var userResponse =  await mediator.Send(new GetUserByIdQuery(UserId), cancellationToken);
            var user = userResponse.Data;
            if (user == null) return null;

            string token = this.jwtGenerator.GenerateJwt(user.UserId, user.Name, user.Email);

            return new ApiResponse<AuthenticationResult>
            (
               new AuthenticationResult(user.UserId, user.Name, user.Email, token)
            );

        }

    }
}
