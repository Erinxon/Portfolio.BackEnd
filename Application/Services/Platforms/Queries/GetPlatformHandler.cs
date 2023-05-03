using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Specifications;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Platforms.Queries
{
    public record GetPlatformQuery : IRequest<ApiResponse<IEnumerable<ViewPlatform>>>;

    public class GetPlatformHandler : IRequestHandler<GetPlatformQuery, ApiResponse<IEnumerable<ViewPlatform>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetPlatformHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<ViewPlatform>>> Handle(GetPlatformQuery request, CancellationToken cancellationToken)
        {
            var Platforms = await fromSqlRaw.GetAllFromSql<ViewPlatform>(new FromSqlRawParams(StoreProcedure.Sp_GetPlatforms, new object[] { null }), cancellationToken);
            return new ApiResponse<IEnumerable<ViewPlatform>>(Platforms);
        }
    }
}
