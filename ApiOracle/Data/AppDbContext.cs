using Microsoft.EntityFrameworkCore;
using ApiOracle.Models;

namespace ApiOracle.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; } = null!;
    }
}
