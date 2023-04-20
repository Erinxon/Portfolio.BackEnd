using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Application.DTOS.AuthResult;
using Application.Tools;
using Domain.Entities;
using Domain.Shared;
using MediatR;


namespace Application.Services.Auth.Commands
{
    public class AuthUserHandler : IRequestHandler<AuthUserCommand, AuthenticationResult>
    {

        private readonly IFromSqlRawGeneric fromSqlRawGeneric;
        private readonly IJwtGenerator jwtGenerator;

        public AuthUserHandler(IFromSqlRawGeneric fromSqlRawGeneric, IJwtGenerator jwtGenerator)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
            this.jwtGenerator = jwtGenerator;   
        }

        public async Task<AuthenticationResult> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            User user = await this.fromSqlRawGeneric
                .GetSingleFromSql<User>(
                new FromSqlRawParams("[dbo].[Sp_AuthUser] {0}, {1}", 
                new object[] { request.Email, request.Password.ToEncryptedPassword() }), 
                cancellationToken);

            if (user == null) return null;

            string token = this.jwtGenerator.GenerateJwt(user.UserId, user.Name, user.Email);

            return new AuthenticationResult
            (
               user.UserId, user.Name, user.Email, token
            );

        }
    }

}
