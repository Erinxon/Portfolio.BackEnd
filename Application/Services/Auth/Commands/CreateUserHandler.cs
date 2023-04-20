using Application.Auth.Queries;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Application.DTOS.AuthResult;
using Application.Tools;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthenticationResult>
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

        public async Task<AuthenticationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            int UserId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync($"exec [dbo].[Sp_CreateUser] '{request.Name}', '{request.Email}', '{request.Password.ToEncryptedPassword()}', @Identity out", cancellationToken);

            User user = await mediator.Send(new GetUserByIdQuery(UserId), cancellationToken);

            if (user == null) return null;

            string token = this.jwtGenerator.GenerateJwt(user.UserId, user.Name, user.Email);

            return new AuthenticationResult
            (
               user.UserId, user.Name, user.Email, token
            );

        }

    }
}
