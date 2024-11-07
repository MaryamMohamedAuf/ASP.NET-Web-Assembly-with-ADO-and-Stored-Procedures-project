using SOFCOproject.Server.Interface;
using SOFCOproject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOFCOproject.Server.Services
{
    public class UserService
    {
        private readonly Iuser _userRepo;

        public UserService(Iuser userRepo)
        {
            _userRepo = userRepo;
        }

        // Get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetUserById(id);
        }

        // Create or Save user
        public async Task<int> SaveUserAsync(User user)
        {
            return await _userRepo.SaveUser(user);
        }

        // Update user
        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _userRepo.UpdateUser(user);
        }

        // Delete user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepo.DeleteUser(id);
        }
    }
}
