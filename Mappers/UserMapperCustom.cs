using ApiDevBP.DTO;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;

namespace ApiDevBP.Mappers
{
    #region AUTOMAPPER

    public class UserAutoMapper(IMapper mapper)
    {
        public UserDTO Map(UserEntity entity)
        {
            // Mapear el modelo User al modelo UserDTO
            UserDTO userDto = mapper.Map<UserDTO>(entity);
            return userDto;
        }

        public UserEntity Map(UserDTO entity)
        {
            // Mapear el modelo User al modelo UserEntity
            UserEntity userDto = mapper.Map<UserEntity>(entity);
            return userDto;
        }
    }

    #endregion

    #region MAPPER CUSTOM

    public static class UserMapperCustom
    {
        public static UserModel Map(UserEntity entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Lastname = entity.Lastname,
                Name = entity.Name
            };
        }

        public static UserEntity Map(UserModel dto)
        {
            return new UserEntity
            {
                Id = dto.Id,
                Lastname = dto.Lastname,
                Name = dto.Name
            };
        }
    }

    #endregion
}
