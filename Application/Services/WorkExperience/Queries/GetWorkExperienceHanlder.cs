using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Specifications;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorkExperience.Queries
{
    public record GetWorkExperienceQuery : IRequest<ApiResponse<IEnumerable<ViewWorkExperience>>>
    {
        public int UserId { get; set; }

        public GetWorkExperienceQuery(int UserId)
        {
            this.UserId = UserId;
        }
    }

    public class GetWorkExperienceHanlder : IRequestHandler<GetWorkExperienceQuery, ApiResponse<IEnumerable<ViewWorkExperience>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public GetWorkExperienceHanlder(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<IEnumerable<ViewWorkExperience>>> Handle(GetWorkExperienceQuery request, CancellationToken cancellationToken)
        {
            var skills = await fromSqlRaw.GetAllFromSql<ViewWorkExperience>(new FromSqlRawParams(StoreProcedure.Sp_GetWorkExperience, new object[] { request.UserId }), cancellationToken);
            return new ApiResponse<IEnumerable<ViewWorkExperience>>(skills);
        }
    }
}