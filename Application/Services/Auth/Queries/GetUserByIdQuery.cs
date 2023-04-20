using Application.DTOs.ApiResponse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth.Queries
{
    public record GetUserByIdQuery : IRequest<ApiResponse<User>>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int UserId)
        {
            this.UserId = UserId;
        }
    }
}
