using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services;

public class AuthService : IAuthService
{
    private UserManager<ApplicationUser> userManager;
    private IConfiguration configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    private string GenerateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("fullName", user.FullName)
        };

        var key = configuration["Jwt:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

   public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
{
    var user = new ApplicationUser
    {
        FullName = registerDto.FullName,
        Email = registerDto.Email,
        UserName = registerDto.Email
    };

    var result = await userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
        return null;
    }

    var token = GenerateToken(user);

    return new AuthResponseDto{
        Token = token,
        Email = user.Email ?? "",
        FullName = user.FullName
    };
}

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto){
    var user = await userManager.FindByEmailAsync(loginDto.Email);

    if (user == null){
        return null;
    }

    var passwordOk = await userManager.CheckPasswordAsync(user, loginDto.Password);

    if (!passwordOk){
        return null;
    }

    var token = GenerateToken(user);

    return new AuthResponseDto
        {
            Token = token,
            Email = user.Email ?? "",
            FullName = user.FullName
        };
    }
}
