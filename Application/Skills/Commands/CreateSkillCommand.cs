using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Skills.Commands
{
    public record CreateSkillCommand(int LanguageId, int LevelId, int UserId) : IRequest<int>;
}
