using Microsoft.AspNetCore.Identity;
using PcBuilder.Entities;

namespace PcBuilder.Services.Interfaces;

public interface IAuthService
{
    public Task<UserEntity?> FindByEmailAsync(string email);
    public Task<SignInResult?> CheckPasswordSignInAsync(UserEntity user, string password, bool lockoutOnFailure);
    public Task<IList<string>?> GetRolesAsync(UserEntity user);
}
