using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private IAuthService authService;

    public AuthController(IAuthService authService){
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto){
        var result = await authService.RegisterAsync(registerDto);

        if (result == null){
            return BadRequest("Register failed");
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto){
        var result = await authService.LoginAsync(loginDto);

        if (result == null){
            return Unauthorized("Invalid email or password");
        }

        return Ok(result);
    }
}