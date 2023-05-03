using MediatR;
using Domain.Entities;
using Application.Common.Interfaces.Persitence;
using Domain.Shared;
using Application.DTOs.ApiResponse;
using Application.Specifications;

namespace Application.Services.Skills.Queries
{

    public class GetAllSkillsQuery : IRequest<ApiResponse<IEnumerable<Skill>>>
    {
        public int UserId { get; set; }

        public GetAllSkillsQuery(int UserId)
        {
            this.UserId = UserId;
        }
    }

    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ApiResponse<IEnumerable<Skill>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetAllSkillsHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<Skill>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await fromSqlRaw.GetAllFromSql<Skill>(new FromSqlRawParams(StoreProcedure.Sp_GetSkills, new object[] { request.UserId }), cancellationToken);
            return new ApiResponse<IEnumerable<Skill>>(skills);
        }

    }
}
