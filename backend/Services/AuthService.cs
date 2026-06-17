using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services;

public class AuthService : IAuthService{
    private UserManager<ApplicationUser> userManager;

    public AuthService(UserManager<ApplicationUser> userManager){
        this.userManager = userManager;
    }
    public Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto){
        return Task.FromResult<AuthResponseDto?>(null);
    }
    public Task<AuthResponseDto?> LoginAsync(LoginDto loginDto){
        return Task.FromResult<AuthResponseDto?>(null);
    }
}
