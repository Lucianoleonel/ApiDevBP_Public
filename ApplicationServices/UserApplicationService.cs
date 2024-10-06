using ApiDevBP.Entities;
using ApiDevBP.Exceptions;
using ApiDevBP.Models;
using ApiDevBP.Repositories;
using ApiDevBP.Validations;
using AutoMapper;

namespace ApiDevBP.ApplicationServices
{
    public class UserApplicationService
    {
        #region Declarations

        private readonly IUserRepository _userApplicationService;
        private readonly IUserValidator _userValidator;
        private readonly IMapper _mapper;

        #endregion

        public UserApplicationService(IUserRepository userApplicationService,
                                        IMapper mapper,
                                        IUserValidator userValidator)
        {
            _userApplicationService = userApplicationService;
            _userValidator = userValidator;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(UserModel userDTO)
        {
            /* validar los datos de entrada */
            _userValidator.Validate(userDTO);
            /*
                paso la logica de los mapeos aqui por lo solicitado pero lo ideal en mi 
                punto de vista seria mejor que las capas interiores no tengan que manejar 
                logica externa
            */
            return await _userApplicationService.AddAsync(_mapper.Map<UserEntity>(userDTO));
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            List<UserEntity> listUserEntity = await _userApplicationService.GetUsersAsync();
            return listUserEntity.Select(userEntity => _mapper.Map<UserModel>(userEntity)).ToList();            
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            UserEntity userEntity = await _userApplicationService.GetUserAsync(id);
            if (userEntity is null)
                throw new UserException($"El usuario {id} no existe");

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<bool> UserExistAsync(int id)
        {
            _userValidator.ValidateUserId(id);
            return await _userApplicationService.ExistAsync(id);
        }


        public async Task UpdateAsync(UserModel userDTO)
        {
            _userValidator.Validate(userDTO);
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            await _userApplicationService.UpdateAsync(userEntity);
        }

        public async Task DeleteAsync(UserModel userDTO)
        {            
            await _userApplicationService.DeleteAsync(_mapper.Map<UserEntity>(userDTO));
        }
    }
}
