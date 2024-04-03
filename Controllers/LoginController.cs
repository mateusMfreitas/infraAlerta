using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using infraAlerta.Helper;

namespace infraAlerta.Controllers;

[ApiController]
[Route("login")]

public class LoginController : Controller
{
    private readonly ApiDbContext _context;
    //private readonly Helper.ISession _session;
    
    public LoginController(ApiDbContext context)
    {
        _context = context;
        //_session = session;
    }

    [HttpPost("auth", Name = "auth")]
    public async Task<IActionResult> Auth([FromBody] LoginModel login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var passwordHash = login.password.GenerateHash(); // Gera o hash da senha fornecida pelo usuário

        var userDb = await _context.User.FirstOrDefaultAsync(x => x.email == login.email && x.password == passwordHash);
        if (userDb == null)
        {
            return NotFound();
        }

        //_session.CreateUserSession(userDb);
        return Ok(userDb);
    }

    [HttpPost("forgotPassword", Name = "forgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userDb = await _context.User.FirstOrDefaultAsync(x => x.email == forgotPassword.email);
        if (userDb == null)
        {
            return NotFound();
        }

        string newPassword = userDb.NewPassword(); // Gera uma nova senha para o usuário

        await _context.SaveChangesAsync();
        // Enviar e-mail com a senha
        return Ok();
    }

    /*
    [HttpGet("logout", Name = "logout")]
    public async Task<IActionResult> Logout()
    {
        _session.DestroyUserSession();
        return Ok();
    }

    [HttpGet("session", Name = "session")]
    public async Task<IActionResult> Session()
    {
        var user = _session.GetUserSession();
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }*/
}
