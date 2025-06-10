using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using projektSBD.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Przykładowi użytkownicy "z palca"
    private readonly Dictionary<string, (string password, string role)> _users = new()
    {
        { "admin", ("admin", "admin") },
        { "client", ("klient", "klient") }
    };

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserCredentials credentials)
    {
        if (_users.TryGetValue(credentials.Username, out var userInfo) &&
            userInfo.password == credentials.Password)
        {
            var token = GenerateJwtToken(credentials.Username, userInfo.role);
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }

    private string GenerateJwtToken(string username, string role)
    {
        var key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
