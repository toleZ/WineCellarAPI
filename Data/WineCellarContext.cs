using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WineCellarContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Tasting> Tasting { get; set; }

        public WineCellarContext(DbContextOptions<WineCellarContext> options) : base(options)
        {   

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasting>()
                .HasMany(s => s.Wines)
                .WithMany();
        }
    }
}
