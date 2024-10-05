using ApiDevBP.Entities;

namespace ApiDevBP.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetUsersAsync();
        Task<UserEntity> GetUserAsync(int id);
        Task<int> AddAsync(UserEntity userEntity);
        Task<bool> ExistAsync(int id);
        Task UpdateAsync(UserEntity userEntity);
        Task DeleteAsync(UserEntity userEntity);
    }
}
