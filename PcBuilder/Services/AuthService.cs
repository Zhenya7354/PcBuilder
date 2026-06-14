using Microsoft.AspNetCore.Identity;
using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;
using System.Net;

namespace PcBuilder.Services;

public class AuthService(
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager,
    IRegularUserRepository regularUserRepository,
    IJwtService jwtService,
    IConfiguration configuration) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly IRegularUserRepository _regularUserRepository = regularUserRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IConfiguration _configuration = configuration;

    public async Task RegisterAsync(RegisterRequest dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            throw new ArgumentNullException("Email or password can not be null or whitespace.");

        var user = new UserEntity { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) 
            throw new InvalidOperationException("Failed to create user.");

        await _userManager.AddToRoleAsync(user, "User");

        await _regularUserRepository.AddRegularUserAsync(new RegularUserEntity { UserId = user.Id }, cancellationToken);
        await _regularUserRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password)) 
            throw new ArgumentNullException("Email or password can not be null or whitespace.");
        var user = await FindByEmailAsync(dto.Email);
        var result = await CheckPasswordSignInAsync(user, dto.Password, true);
        var roles = await GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user, roles);
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration.GetSection("Jwt")["ExpiresInMinutes"]!));
        return new AuthResponse(token, user.Email!, roles, expires);
    }

    private async Task<UserEntity> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    private async Task<SignInResult> CheckPasswordSignInAsync(UserEntity user, string password, bool lockoutOnFailure)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        if (result is null)
            throw new BadHttpRequestException("Invalid email or password.");

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                throw new HttpRequestException("Account locked. Try again later.",inner: null, statusCode: HttpStatusCode.TooManyRequests);
            throw new UnauthorizedAccessException();
        }
        return result;
    }

    private async Task<IList<string>> GetRolesAsync(UserEntity user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        if (roles is null)
            throw new BadHttpRequestException("Failed to retrieve user roles.");
        return roles;
    }

}
