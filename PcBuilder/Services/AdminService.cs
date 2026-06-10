using Microsoft.AspNetCore.Identity;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class AdminService(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IAdminRepository adminRepository) : IAdminService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IAdminRepository _adminRepository = adminRepository;

    public async Task PromoteToAdminAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
            throw new KeyNotFoundException("User not found.");

        await _userManager.AddToRoleAsync(user, "Admin");
        await _adminRepository.AddAdminAsync(new AdminEntity { UserId = user.Id }, cancellationToken);
        await _adminRepository.SaveChangesAsync(cancellationToken);

    }
}
