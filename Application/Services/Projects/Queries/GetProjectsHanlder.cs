using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.DTOs.Proyects;
using Application.Services.ProyectSkills.Queries;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Proyects.Queries
{
    public record GetProjectsQuery : IRequest<ApiResponse<IEnumerable<GetProyectDto>>>
    {
        public int UserId { get; set; }

        public GetProjectsQuery(int UserId)
        {
            this.UserId = UserId;
        }
    }

    public class GetProjectsHanlder : IRequestHandler<GetProjectsQuery, ApiResponse<IEnumerable<GetProyectDto>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetProjectsHanlder(IFromSqlRawGeneric fromSqlRaw, IMediator mediato, IMapper mapper)
        {
            this.fromSqlRaw = fromSqlRaw;
            this._mediator = mediato;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetProyectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var proyects = await fromSqlRaw.GetAllFromSql<ViewProyect>(new FromSqlRawParams(StoreProcedure.Sp_GetProjects, new object[] { request.UserId }), cancellationToken);
           
            var response = new ApiResponse<IEnumerable<GetProyectDto>>(_mapper.Map<IEnumerable<GetProyectDto>>(proyects));
            foreach (var proyect in response.Data)
            {
                proyect.ProyectSkills = await this._mediator.Send(new GetProjectSkillQuery(proyect.ProyectId), cancellationToken);
            }
            return response;
        }
    }
}
