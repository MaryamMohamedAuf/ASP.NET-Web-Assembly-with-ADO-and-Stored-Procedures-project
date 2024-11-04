using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using SOFCOproject.Shared;
using SOFCOproject.Shared.Models;


namespace SOFCOproject.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/user/{id}");
        }
        public async Task<List<User>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/user");
        }

        public async Task<User> CreateUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task UpdateUser(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user/{user.Id}", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/user/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
