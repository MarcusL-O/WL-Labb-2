using Microsoft.EntityFrameworkCore;
using Wl_labb2.Models;

namespace Wl_labb2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Snus> SnusItems { get; set; }
    }
}
