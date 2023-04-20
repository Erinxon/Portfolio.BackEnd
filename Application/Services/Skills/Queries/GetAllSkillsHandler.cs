using MediatR;
using Domain.Entities;
using Application.Common.Interfaces.Persitence;
using Domain.Shared;
using Application.DTOs.ApiResponse;

namespace Application.Services.Skills.Queries
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ApiResponse<IEnumerable<Skill>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetAllSkillsHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<Skill>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await fromSqlRaw.GetAllFromSql<Skill>(new FromSqlRawParams("[dbo].[Sp_GetSkills] {0}", new object[] { null }), cancellationToken);
            return new ApiResponse<IEnumerable<Skill>>(skills);
        }

    }
}
