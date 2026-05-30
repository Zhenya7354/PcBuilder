using Microsoft.AspNetCore.Identity;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class AdminService(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IAdminRepository adminRepository, ITransactionService transactionService) : IAdminService
{
    private readonly UserManager<UserEntity> _userManager= userManager;
    private readonly RoleManager<IdentityRole> _roleManager= roleManager;
    private readonly IAdminRepository _adminRepository= adminRepository;
    private readonly ITransactionService _transactionService = transactionService;
    public async Task PromoteToAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
            throw new KeyNotFoundException("User not found.");

        using var transaction = await _transactionService.BeginTransactionAsync();
        try
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            await _adminRepository.AddAdminAsync(new AdminEntity { UserId = user.Id });
            await _adminRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
