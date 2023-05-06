using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProyectSkills.Commands
{
    public record CreateProjectSkillCommand(int ProyectSkillId, int ProyectId, int SkillId) : IRequest<ApiResponse<int>>;

    public class CreateProjectSkillHanlder : IRequestHandler<CreateProjectSkillCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateProjectSkillHanlder(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<ApiResponse<int>> Handle(CreateProjectSkillCommand request, CancellationToken cancellationToken)
        {
            var ProyectSkillId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync(StoreProcedure.Sp_SetProjectSkills, request, cancellationToken);
            return new ApiResponse<int>(ProyectSkillId);
        }
    }
}
