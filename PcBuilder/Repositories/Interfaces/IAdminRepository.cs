namespace PcBuilder.Repositories.Interfaces;

public interface IAdminRepository
{
    public Task<List<AdminEntity>> GetAdminsAsync(CancellationToken cancellationToken);
    public Task<AdminEntity?> GetAdminByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddAdminAsync(AdminEntity admin, CancellationToken cancellationToken);
    public Task DeleteAdminAsync(AdminEntity admin, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
