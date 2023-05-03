using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorkExperience.Commands
{
    public record CreateWorkExperienceCommand(int WorkExperienceId, string CompanyName, string PositionName, string Description, int UserId, DateTime StartDate, DateTime? EndDate = null) 
        : IRequest<ApiResponse<int>>;

    public class CreateWorkExperienceHandler : IRequestHandler<CreateWorkExperienceCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;

        public CreateWorkExperienceHandler(IFromSqlRawGeneric fromSqlRaw)
        {
            this.fromSqlRaw = fromSqlRaw;
        }

        public async Task<ApiResponse<int>> Handle(CreateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var WorkExperienceId = await this.fromSqlRaw.ExecuteSqlRawAsync(StoreProcedure.Sp_SetWorkExperience, request, cancellationToken);
            return new ApiResponse<int>(WorkExperienceId);
        }
    }
}

