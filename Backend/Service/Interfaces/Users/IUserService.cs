using Data.Models.Users;

namespace Service.Interfaces.Users
{
    interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<UserProfileDetailed> GetUserProfileDetailedById(int id);
        Task<UserProfileSummary> GetUserProfileSummaryByUsername(string username);
        Task<int> RegisterUser(User user);
        Task<int> LoginUser(string username, string password);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> DeactivateUser(int id);
    }
}
