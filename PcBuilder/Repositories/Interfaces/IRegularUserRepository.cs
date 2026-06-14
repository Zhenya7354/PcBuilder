using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IRegularUserRepository
{
    public Task<List<RegularUserEntity>> GetRegularUsersAsync(CancellationToken cancellationToken);
    
    public Task<RegularUserEntity?> GetRegularUserByIdAsync(int id, CancellationToken cancellationToken);

    public Task AddRegularUserAsync(RegularUserEntity dto, CancellationToken cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken);

}
