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
    public class GetProjectSkillQuery : IRequest<IEnumerable<ViewProyectSkill>>
    {
        public int ProyectId { get; set; }

        public GetProjectSkillQuery(int ProyectId)
        {
            this.ProyectId = ProyectId;
        }
    }

    public class GetProjectSkillHandler : IRequestHandler<GetProjectSkillQuery, IEnumerable<ViewProyectSkill>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetProjectSkillHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<IEnumerable<ViewProyectSkill>> Handle(GetProjectSkillQuery request, CancellationToken cancellationToken)
        {
            var ProyectSkills = await fromSqlRaw.GetAllFromSql<ViewProyectSkill>(new FromSqlRawParams(StoreProcedure.Sp_GetProjectSkills, new object[] { request.ProyectId }), cancellationToken);
            return ProyectSkills;
        }
    }
}
