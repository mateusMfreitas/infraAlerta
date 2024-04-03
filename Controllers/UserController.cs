using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace infraAlerta.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
        private readonly ApiDbContext _context;

    public UserController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet("getUsers", Name = "getUsers")]
    public IActionResult GetUsers()
    {
        var users = _context.User.ToList();
        return Ok(users);
    }

    [HttpPost("createUser", Name = "createUser")]

    public async Task<IActionResult> CreateUser([FromBody] User newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        newUser.SetPasswordHash();
        _context.User.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(newUser);
    }

    [HttpPut("updateUser/{id}", Name = "updateUser")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _context.User.FirstOrDefault(x => x.user_id == id);
        if (user == null)
        {
            return NotFound();
        }

        user.name = updatedUser.name;
        user.cpf = updatedUser.cpf;
        user.phone = updatedUser.phone;
        user.admin = updatedUser.admin;
        user.birthDate = updatedUser.birthDate;
        user.email = updatedUser.email;
        user.login = updatedUser.login;

        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpDelete("deleteUser/{id}", Name = "deleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = _context.User.FirstOrDefault(x => x.user_id == id);
        if (user == null)
        {
            return NotFound();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
