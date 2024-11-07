using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SOFCOproject.Shared.Models;
using Dapper;
using SOFCOproject.Server.Interface;

namespace SOFCOproject.Server.Services
{
    public class UserRepo:Iuser
    {
        private readonly string _connectionString;

        public UserRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "EXEC ViewUsers"; // Call your stored procedure
                var users = await connection.QueryAsync<User>(query);
                return users.AsList();
            }
        }
        public async Task<User> GetUserById(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetUserById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
            }
            return null;
        }
        public async Task<int> SaveUser(User user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("SaveUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters for the stored procedure
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            await connection.OpenAsync();
            int rowsAffected = (int)await command.ExecuteScalarAsync();
            return rowsAffected;
        }

        // Update an existing user
        public async Task<bool> UpdateUser(User user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Add parameters for the stored procedure
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0; // Returns true if at least one row was updated
        }

        // Delete a user by ID
        public async Task<bool> DeleteUser(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("DeleteUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}