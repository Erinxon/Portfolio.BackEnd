using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using MediatR;

namespace Application.Services.Skills.Commands
{
    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateSkillHandler(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<ApiResponse<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skillId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync($"exec [dbo].[Sp_SetSkills] {request.LanguageId}, {request.LevelId}, {request.UserId}, @Identity out", cancellationToken);
            return new ApiResponse<int>(skillId);
        }

    }
}