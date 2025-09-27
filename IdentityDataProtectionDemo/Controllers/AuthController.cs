using IdentityDataProtectionDemo.Data;
using IdentityDataProtectionDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityDataProtectionDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext context, PasswordHasher<User> hasher) : ControllerBase
{
    public record RegisterDto(string Email, string Password);

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var exists = await context.Users.AnyAsync(u => u.Email == dto.Email);
        if (exists) return Conflict("Email already exists.");

        var user = new User { Email = dto.Email };
        user.Password = hasher.HashPassword(user, dto.Password); // HASH!

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok(new { user.Id, user.Email });
    }

    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        // Debug için listele (parola hash’lerini de görebilirsin)
        var list = await context.Users
            .Select(u => new { u.Id, u.Email, u.Password })
            .ToListAsync();
        return Ok(list);
    }
}
