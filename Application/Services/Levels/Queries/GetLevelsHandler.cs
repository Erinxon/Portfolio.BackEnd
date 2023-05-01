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

namespace Application.Services.Levels.Queries
{
    public record GetLevelsQuery : IRequest<ApiResponse<IEnumerable<ViewLevel>>>;

    public class GetLevelsHandler : IRequestHandler<GetLevelsQuery, ApiResponse<IEnumerable<ViewLevel>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetLevelsHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<ViewLevel>>> Handle(GetLevelsQuery request, CancellationToken cancellationToken)
        {
            var Levels = await fromSqlRaw.GetAllFromSql<ViewLevel>(new FromSqlRawParams("[dbo].[Sp_GetLevels] {0}", new object[] { null }), cancellationToken);
            return new ApiResponse<IEnumerable<ViewLevel>>(Levels);
        }
    }
}
