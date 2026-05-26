using PcBuilder.Entities;

namespace PcBuilder.Services.Interfaces;

public interface IJwtService
{
    public string GenerateToken(UserEntity user, IList<string> roles);
}
