using CataasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CataasApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SearchHistory> SearchHistories { get; set; } = null!;

    }
}
