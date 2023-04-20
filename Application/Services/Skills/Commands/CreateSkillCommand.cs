using Application.DTOs.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Skills.Commands
{
    public record CreateSkillCommand(int LanguageId, int LevelId, int UserId) : IRequest<ApiResponse<int>>;
}
