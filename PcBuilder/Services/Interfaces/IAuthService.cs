using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IAuthService
{
    public Task RegisterAsync(RegisterRequest dto, CancellationToken cancellationToken = default);
    public Task<AuthResponse> LoginAsync(LoginRequest dto, CancellationToken cancellationToken = default);
}
