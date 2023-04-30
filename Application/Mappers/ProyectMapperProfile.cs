using Application.DTOs.Proyects;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ProyectMapperProfile : Profile
    {
        public ProyectMapperProfile()
        {
            CreateMap<ViewProyect, GetProyectDto>();
        }
    }
}
