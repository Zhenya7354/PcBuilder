using Microsoft.EntityFrameworkCore;

namespace PcBuilder.Data
{
    public class PcDbContext(DbContextOptions<PcDbContext> options) : DbContext(options)
    {

    }
}
