using ApiDevBP.DTO;
using ApiDevBP.Entities;
using AutoMapper;

namespace ApiDevBP.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDTO>();
            CreateMap<UserDTO,UserEntity>();
        }
    }
}
