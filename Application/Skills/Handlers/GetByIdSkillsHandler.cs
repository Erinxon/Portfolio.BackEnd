using Application.Common.Interfaces.Persitence;
using Application.Skills.Queries;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Skills.Handlers
{
    public class GetByIdSkillsHandler : IRequestHandler<GetByIdSkillsQuery, Skill>
    {
        private readonly IFromSqlRawGeneric _fromSqlRawGenery;

        public GetByIdSkillsHandler(IFromSqlRawGeneric fromSqlRawGenery)
        {
            this._fromSqlRawGenery = fromSqlRawGenery;
        }

        public async Task<Skill> Handle(GetByIdSkillsQuery request, CancellationToken cancellationToken)
        {
            return await this._fromSqlRawGenery.GetSingleFromSql<Skill>(new FromSqlRawParams("[dbo].[Sp_GetSkills] {0}", new object[] { request.SkillId }), cancellationToken);
        }
    }
}
