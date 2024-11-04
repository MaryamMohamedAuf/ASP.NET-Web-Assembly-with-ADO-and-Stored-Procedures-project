using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SOFCOproject.Shared;
using SOFCOproject.Server.Services;
using SOFCOproject.Shared.Models;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        if (users == null || users.Count == 0)
        {
            return NotFound("No users found.");
        }
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult> SaveUser([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("User name and email are required.");
        }
        await _userService.SaveUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    // PUT: api/User/{id} - Update an existing user
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (user == null || id != user.Id)
        {
            return BadRequest("User ID mismatch.");
        }
        bool isUpdated = await _userService.UpdateUser(user);
        if (!isUpdated)
        {
            return NotFound("User not found or no changes were made.");
        }
        return NoContent(); // Returns 204 No Content on success
    }

    // DELETE: api/User/{id} - Delete a user by ID
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        bool isDeleted = await _userService.DeleteUser(id);
        if (!isDeleted)
        {
            return NotFound("User not found.");
        }
        return NoContent();
    }

}
