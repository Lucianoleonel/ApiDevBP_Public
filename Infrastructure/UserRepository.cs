using ApiDevBP.Configuration;
using ApiDevBP.Entities;
using ApiDevBP.Repositories;
using Microsoft.Extensions.Options;
using SQLite;

namespace ApiDevBP.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        //esto se podria hacer por Inyeccion por Dependencia
        private readonly SQLiteConnection _db;
        private string query = "Select * from Users";

        public UserRepository(IOptions<ConfigurationDB> dbOptions)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string _connectionString = Path.Combine(basePath, dbOptions.Value.ConnectionString);       
            _db = new SQLiteConnection(_connectionString);
            _db.CreateTable<UserEntity>();
        }

        #region Methods DB

        public async Task<int> AddAsync(UserEntity user)
        {
            int result = 0;
            await Task.Run(() =>
            {
                result = _db.Insert(user);
            });
            return result;
        }

        public Task<List<UserEntity>> GetUsersAsync()
        {
            var users = _db.Query<UserEntity>(query);
            return Task.FromResult(users);
        }

        public async Task<bool> ExistAsync(int id)
        {
            var users = _db.Query<int>($"Select 1 from Users where id = {id}");
            return Task.FromResult(users?.Count > 0).Result;
        }

        public async Task<UserEntity> GetUserAsync(int id)
                => _db.Query<UserEntity>($"{query} where id = {id}").FirstOrDefault();


        public async Task UpdateAsync(UserEntity userEntity)
        {
            _db.Update(userEntity);
        }

        public async Task DeleteAsync(UserEntity userEntity)
        {
            _db.Delete(userEntity);
        }

        #endregion              

    }
}
