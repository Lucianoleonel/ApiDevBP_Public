using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;

namespace ApiDevBP.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname));

            CreateMap<UserModel, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname));

        }
    }
}
