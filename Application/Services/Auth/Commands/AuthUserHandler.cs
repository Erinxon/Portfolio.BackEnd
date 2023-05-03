using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.DTOS.AuthResult;
using Application.Specifications;
using Application.Tools;
using Domain.Shared;
using MediatR;
using Domain.Entities;

namespace Application.Services.Auth.Commands
{
    public record AuthUserCommand(string Email, string Password) : IRequest<ApiResponse<AuthenticationResult>>;

    public class AuthUserHandler : IRequestHandler<AuthUserCommand, ApiResponse<AuthenticationResult>>
    {

        private readonly IFromSqlRawGeneric fromSqlRawGeneric;
        private readonly IJwtGenerator jwtGenerator;

        public AuthUserHandler(IFromSqlRawGeneric fromSqlRawGeneric, IJwtGenerator jwtGenerator)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
            this.jwtGenerator = jwtGenerator;   
        }

        public async Task<ApiResponse<AuthenticationResult>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            User user = await this.fromSqlRawGeneric
                .GetSingleFromSql<User>(
                new FromSqlRawParams(StoreProcedure.Sp_AuthUser, 
                new object[] { request.Email, request.Password.ToEncryptedPassword() }), 
                cancellationToken);

            if (user == null)
            {
                return new ApiResponse<AuthenticationResult>(ConstErrorCode.Auth401, ConstStatusCodes.Code401);
            };

            string token = this.jwtGenerator.GenerateJwt(user.UserId, user.Name, user.Email);

            return new ApiResponse<AuthenticationResult>
            (
               new AuthenticationResult(user.UserId, user.Name, user.Email, token)
            );

        }
    }

}
