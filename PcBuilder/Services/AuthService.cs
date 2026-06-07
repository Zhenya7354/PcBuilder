using Microsoft.AspNetCore.Identity;
using PcBuilder.Entities;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;


    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<SignInResult?> CheckPasswordSignInAsync(UserEntity user, string password, bool lockoutOnFailure)
    {
        return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public async Task<IList<string>?> GetRolesAsync(UserEntity user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}
