using Application.Common.Interfaces.Persitence;
using Application.DTOs.ApiResponse;
using Application.Services.ProyectSkills.Commands;
using Application.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Proyects.Commands
{
    public record CreateProjectCommand : IRequest<ApiResponse<int>>
    {
        public int ProyectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ImageGuidId { get; set; }
        public string GithubUrl { get; set; }
        public string DomainUrl { get; set; }
        public int PlatformId { get; set; }
        public int UserId { get; set; }
        public List<CreateProjectSkillCommand> CreateProyectSkillCommands { get; set; }
    }

    public class CreateProjectSkillHandler : IRequestHandler<CreateProjectCommand, ApiResponse<int>>
    {
        private readonly IFromSqlRawGeneric fromSqlRawGeneric;
        private readonly IMediator _mediator;

        public CreateProjectSkillHandler(IFromSqlRawGeneric fromSqlRawGeneric, IMediator mediator)
        {
            this.fromSqlRawGeneric = fromSqlRawGeneric;
            this._mediator = mediator;
        }

        public async Task<ApiResponse<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            await this.fromSqlRawGeneric.BeginTransactionAsync(cancellationToken);
            try
            {
                var ProyectId = await this.fromSqlRawGeneric.ExecuteSqlRawAsync(StoreProcedure.Sp_SetProject, request, cancellationToken);
                foreach (var ProyectSkill in request?.CreateProyectSkillCommands)
                {
                    CreateProjectSkillCommand createProyectSkill = ProyectSkill with { ProyectId = ProyectId };
                    await _mediator.Send(createProyectSkill, cancellationToken);
                }
                await this.fromSqlRawGeneric.CommitTransactionAsync(cancellationToken);
                return new ApiResponse<int>(ProyectId);
            }
            catch (Exception ex)
            {
                await this.fromSqlRawGeneric.RollbackTransactionAsync(cancellationToken);
                return new ApiResponse<int>(ConstErrorCode.Create400, ConstStatusCodes.Code400);
            }
        }
    }
}
