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
    public class GetByIdSkillsQuery : IRequest<ApiResponse<Skill>>
    {
        public int SkillId { get; set; }

        public GetByIdSkillsQuery(int skillId)
        {
            SkillId = skillId;  
        }
    }
}
