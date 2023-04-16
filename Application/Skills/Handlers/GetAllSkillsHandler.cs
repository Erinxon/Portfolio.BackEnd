using Application.Skills.Queries;
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces.Persitence;
using Domain.Shared;

namespace Application.Skills.Handlers
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, IEnumerable<Skill>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetAllSkillsHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<IEnumerable<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            return await this.fromSqlRaw.GetAllFromSql<Skill>(new FromSqlRawParams("[dbo].[Sp_GetSkills] {0}", new object[] { null }), cancellationToken);
        }

    }
}
