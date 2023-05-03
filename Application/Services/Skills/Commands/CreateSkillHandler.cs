using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Specifications;
using MediatR;

namespace Application.Services.Skills.Commands
{
    public record CreateSkillCommand(int LanguageId, int LevelId, int UserId) : IRequest<ApiResponse<int>>;

    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateSkillHandler(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<ApiResponse<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skillId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync(StoreProcedure.Sp_SetSkills, request, cancellationToken);
            return new ApiResponse<int>(skillId);
        }

    }
}