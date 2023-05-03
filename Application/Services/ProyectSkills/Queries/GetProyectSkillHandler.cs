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

namespace Application.Services.ProyectSkills.Queries
{
    public class GetProyectSkillQuery : IRequest<IEnumerable<ViewProyectSkill>>
    {
        public int ProyectId { get; set; }

        public GetProyectSkillQuery(int ProyectId)
        {
            this.ProyectId = ProyectId;
        }
    }

    public class GetProyectSkillHandler : IRequestHandler<GetProyectSkillQuery, IEnumerable<ViewProyectSkill>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetProyectSkillHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<IEnumerable<ViewProyectSkill>> Handle(GetProyectSkillQuery request, CancellationToken cancellationToken)
        {
            var ProyectSkills = await fromSqlRaw.GetAllFromSql<ViewProyectSkill>(new FromSqlRawParams(StoreProcedure.Sp_GetProyectSkills, new object[] { request.ProyectId }), cancellationToken);
            return ProyectSkills;
        }
    }
}
