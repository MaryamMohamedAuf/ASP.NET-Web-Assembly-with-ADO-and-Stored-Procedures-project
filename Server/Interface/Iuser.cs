using SOFCOproject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOFCOproject.Server.Interface
{
    public interface Iuser
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserById(int id);
        Task<int> SaveUser(User user); // Returns int for rows affected
        Task<bool> UpdateUser(User user); // Returns bool to indicate success
        Task<bool> DeleteUser(int id); // Returns bool to indicate success
    }
}
