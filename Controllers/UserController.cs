using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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

    public async Task<IActionResult> CreateUser([FromBody] UserCreationData creationData)
    {

        creationData.User.SetPasswordHash(); // Define o hash da senha usando o método definido na classe User
        _context.User.Add(creationData.User);
        await _context.SaveChangesAsync();

        var userId = creationData.User.user_id;
        creationData.UserAddress.ua_user_id = userId;
        _context.User_Address.Add(creationData.UserAddress);
        await _context.SaveChangesAsync();
        
        return Ok(userId);

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

    [HttpPut("changePassword/{id}", Name = "changePassword")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] changePasswordModel updatedPassword)
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

        user.password = updatedPassword.newPassword;
        user.SetPasswordHash();

        await _context.SaveChangesAsync();

        return Ok();
    }
}

public class UserCreationData
{
    public User User { get; set; }
    public User_address UserAddress { get; set; }
}



