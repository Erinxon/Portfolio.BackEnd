using Application.DTOs.ApiResponse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Skills.Queries
{
    public class GetAllSkillsQuery : IRequest<ApiResponse<IEnumerable<Skill>>>
    {
    }
}
