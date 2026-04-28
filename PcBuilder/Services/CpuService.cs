using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class CpuService(PcBuilderDbContext context)
    {
        private readonly PcBuilderDbContext _context = context;
        public async Task<List<Cpu>> GetAllCpuAsync()
        {
            return await _context.Cpus.Include(c => c.Brand).ToListAsync();
        }
    }
}
