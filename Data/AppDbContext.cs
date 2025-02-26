using Clima.Models;
using Microsoft.EntityFrameworkCore;

namespace Clima.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClimaModel> Clima { get; set; }
    }
}
