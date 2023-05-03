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
    public record GetProyectsQuery : IRequest<ApiResponse<IEnumerable<GetProyectDto>>>
    {
        public int UserId { get; set; }

        public GetProyectsQuery(int UserId)
        {
            this.UserId = UserId;
        }
    }

    public class GetProyectsHanlder : IRequestHandler<GetProyectsQuery, ApiResponse<IEnumerable<GetProyectDto>>>
    {
        private readonly IFromSqlRawGeneric fromSqlRaw;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetProyectsHanlder(IFromSqlRawGeneric fromSqlRaw, IMediator mediato, IMapper mapper)
        {
            this.fromSqlRaw = fromSqlRaw;
            this._mediator = mediato;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetProyectDto>>> Handle(GetProyectsQuery request, CancellationToken cancellationToken)
        {
            var proyects = await fromSqlRaw.GetAllFromSql<ViewProyect>(new FromSqlRawParams(StoreProcedure.Sp_GetProyects, new object[] { request.UserId }), cancellationToken);
           
            var response = new ApiResponse<IEnumerable<GetProyectDto>>(_mapper.Map<IEnumerable<GetProyectDto>>(proyects));
            await Task.Run(async () =>
            {
                foreach (var proyect in response.Data)
                {
                   proyect.ProyectSkills = await this._mediator.Send(new GetProyectSkillQuery(proyect.ProyectId), cancellationToken);
                }
            });
            return response;
        }
    }
}
