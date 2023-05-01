using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Services.Languages.Queries
{
    public record GetLanguagesQuery : IRequest<ApiResponse<IEnumerable<ViewLanguage>>>;

    public class GetLanguagesHandler : IRequestHandler<GetLanguagesQuery, ApiResponse<IEnumerable<ViewLanguage>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetLanguagesHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<ViewLanguage>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var Languages = await fromSqlRaw.GetAllFromSql<ViewLanguage>(new FromSqlRawParams("[dbo].[Sp_GetLanguages] {0}", new object[] { null }), cancellationToken);
            return new ApiResponse<IEnumerable<ViewLanguage>>(Languages);
        }
    }
}
