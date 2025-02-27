using Data.Data.Users;
using Data.Models.Users;
using Service.Interfaces.Users;

namespace Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<UserProfileDetailed> GetUserProfileDetailedById(int id)
        {
            return await _userRepository.GetUserProfileDetailedByIdAsync(id);
        }

        public async Task<UserProfileSummary> GetUserProfileSummaryByUsername(string username)
        {
            return await _userRepository.GetUserProfileSummaryByUsernameAsync(username);
        }

        public async Task<int> RegisterUser(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }
        public async Task<int> LoginUser(string username, string password)
        {
            return await _userRepository.LoginUserAsync(username, password);
        }
        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
        public async Task<bool> DeactivateUser(int id)
        {
            return await _userRepository.DeactivateUserByIdAsync(id);
        }
    }
}
