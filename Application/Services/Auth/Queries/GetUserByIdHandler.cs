using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<User>>
    {

        private readonly IFromSqlRawGeneric _fromSqlRawGenery;

        public GetUserByIdHandler(IFromSqlRawGeneric fromSqlRawGenery)
        {
            this._fromSqlRawGenery = fromSqlRawGenery;
        }

        public async  Task<ApiResponse<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<User>();
            response.Data = await this._fromSqlRawGenery.GetSingleFromSql<User>(new FromSqlRawParams("[dbo].[Sp_GetUsers] {0}", new object[] { request.UserId }), cancellationToken);
            return response;
        }
    }
}
