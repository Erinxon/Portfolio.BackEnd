using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Services.Skills.Queries
{
    public class GetByIdSkillsHandler : IRequestHandler<GetByIdSkillsQuery, ApiResponse<Skill>>
    {
        private readonly IFromSqlRawGeneric _fromSqlRawGenery;

        public GetByIdSkillsHandler(IFromSqlRawGeneric fromSqlRawGenery)
        {
            _fromSqlRawGenery = fromSqlRawGenery;
        }

        public async Task<ApiResponse<Skill>> Handle(GetByIdSkillsQuery request, CancellationToken cancellationToken)
        {
            var skill = await _fromSqlRawGenery.GetSingleFromSql<Skill>(new FromSqlRawParams("[dbo].[Sp_GetSkills] {0}", new object[] { request.SkillId }), cancellationToken);

            return new ApiResponse<Skill>(skill);
        }
    }
}
