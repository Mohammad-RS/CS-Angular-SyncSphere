using Dapper;
using Data.Models.Users;
using Data.Repositories;
using Data.Repositories.Users;

namespace Data.Data.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DataAccess _dataAccess;
        private readonly IGenericRepository _genericRepository;

        public UserRepository(DataAccess dataAccess, IGenericRepository genericRepository)
        {
            _dataAccess = dataAccess;
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _genericRepository.GetAllAsync<User>();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync<User>(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            string query = "SELECT * FROM dbo.[User] WHERE UserName = @Username";
            
            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.QuerySingleAsync<User>(query, new { Username = username });
            }
        }

        public async Task<UserProfileDetailed> GetUserProfileDetailedByIdAsync(int id)
        {
            string query = "SELECT UserName, Email, FullName, Avatar, CreatedAt, LastLogin FROM dbo.[User] WHERE Id = @Id ";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.QuerySingleAsync<UserProfileDetailed>(query, new { Id = id });
            }
        }

        public async Task<UserProfileSummary> GetUserProfileSummaryByUsernameAsync(string username)
        {
            string query = "SELECT UserName, FullName, Avatar, LastLogin FROM dbo.[User] WHERE UserName = @Username ";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.QuerySingleAsync<UserProfileSummary>(query, new { Username = username });
            }
        }

        public async Task<int> CreateUserAsync(User user)
        {
            return await _genericRepository.CreateAsync(user);
        }

        public async Task<int> LoginUserAsync(string username, string password)
        {
            string query = "SELECT Id FROM dbo.[User] WHERE UserName = @Username AND Password = @Password";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.ExecuteScalarAsync<int>(query, new { Username = username, Password = password });
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _genericRepository.UpdateByIdAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _genericRepository.DeleteByIdAsync<User>(id);
        }

        public async Task<bool> DeactivateUserByIdAsync(int id)
        {
            string query = "UPDATE dbo.[User] SET IsActive = 0 WHERE Id = @Id";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.ExecuteAsync(query, new { Id = id }) > 0;
            }
        }
    }
}
