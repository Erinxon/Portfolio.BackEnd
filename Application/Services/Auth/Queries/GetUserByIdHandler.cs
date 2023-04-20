using Application.Common.Interfaces.Persitence;
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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {

        private readonly IFromSqlRawGeneric _fromSqlRawGenery;

        public GetUserByIdHandler(IFromSqlRawGeneric fromSqlRawGenery)
        {
            this._fromSqlRawGenery = fromSqlRawGenery;
        }

        public async  Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await this._fromSqlRawGenery.GetSingleFromSql<User>(new FromSqlRawParams("[dbo].[Sp_GetUsers] {0}", new object[] { request.UserId }), cancellationToken);
        }
    }
}
