using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackendServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    public UserController(
            IConfiguration config)
    {
        _config = config;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<string> Authencate()
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Email,"admin@example.com"),
                new Claim(ClaimTypes.GivenName,"Duong Thanh Phet"),
                new Claim(ClaimTypes.Role, string.Join(";","admin")),
                new Claim(ClaimTypes.Name, "phetdt")
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}