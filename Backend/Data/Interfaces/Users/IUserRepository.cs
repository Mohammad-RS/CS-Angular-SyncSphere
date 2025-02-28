using Data.Models.Users;

namespace Data.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<UserProfileDetailed> GetUserProfileDetailedByIdAsync(int id);
        Task<UserProfileSummary> GetUserProfileSummaryByUsernameAsync(string username);
        Task<int> CreateUserAsync(User user);
        Task<int> LoginUserAsync(string username, string password);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeactivateUserByIdAsync(int id);
    }
}
