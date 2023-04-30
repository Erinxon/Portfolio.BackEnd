using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProyectSkills.Commands
{
    public record CreateProyectSkillCommand(int ProyectId, int SkillId) : IRequest<ApiResponse<int>>;

    public class CreateProyectSkillHanlder : IRequestHandler<CreateProyectSkillCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateProyectSkillHanlder(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<ApiResponse<int>> Handle(CreateProyectSkillCommand request, CancellationToken cancellationToken)
        {
            var ProyectSkillId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync($"exec [dbo].[Sp_SetProyectSkills] 0, {request.ProyectId}, {request.SkillId}, @Identity out", cancellationToken);
            return new ApiResponse<int>(ProyectSkillId);
        }
    }
}
