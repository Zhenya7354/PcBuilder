namespace PcBuilder.Repositories.Interfaces;

public interface IAdminRepository
{
    public Task<List<AdminEntity>> GetAdminsAsync();
    public Task<AdminEntity?> GetAdminByIdAsync(int id);
    public Task AddAdminAsync(AdminEntity admin);
    public Task DeleteAdminAsync(AdminEntity admin);
    public Task SaveChangesAsync();
}
