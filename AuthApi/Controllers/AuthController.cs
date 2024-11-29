using Microsoft.AspNetCore.Mvc;
using AuthApi.Data;
using AuthApi.Models;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthDbContext _context;

    public AuthController(AuthDbContext context)
    {
        _context = context;
    }

    // Signup
    [HttpPost("signup")]
    public IActionResult Signup(User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
            return BadRequest("User already exists.");

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("User registered successfully.");
    }

    // Login
    [HttpPost("login")]
    public IActionResult Login(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

        if (existingUser == null)
            return Unauthorized("Invalid credentials.");

        return Ok("Login successful.");
    }
}

