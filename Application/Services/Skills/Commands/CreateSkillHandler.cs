using Application.Common.Interfaces.Persitence;
using MediatR;

namespace Application.Services.Skills.Commands
{
    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;

        public CreateSkillHandler(IFromSqlRawGeneric fromSqlRawGeneric)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
        }

        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            return await this.fromSqlRawGeneric.ExecuteSqlRawAsync($"exec [dbo].[Sp_SetSkills] {request.LanguageId}, {request.LevelId}, {request.UserId}, @Identity out", cancellationToken);
        }

    }
}