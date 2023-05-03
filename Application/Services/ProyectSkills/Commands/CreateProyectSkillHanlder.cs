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
    public record CreateProyectSkillCommand(int ProyectSkillId, int ProyectId, int SkillId) : IRequest<ApiResponse<int>>;

    public class CreateProyectSkillHanlder : IRequestHandler<CreateProyectSkillCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateProyectSkillHanlder(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<ApiResponse<int>> Handle(CreateProyectSkillCommand request, CancellationToken cancellationToken)
        {
            var ProyectSkillId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync(StoreProcedure.Sp_SetProyectSkills, request, cancellationToken);
            return new ApiResponse<int>(ProyectSkillId);
        }
    }
}
