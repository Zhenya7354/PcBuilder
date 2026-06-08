namespace PcBuilder.Services.Interfaces;

public interface IAdminService
{
    public Task PromoteToAdminAsync(string userId, CancellationToken cancellationToken);
}
