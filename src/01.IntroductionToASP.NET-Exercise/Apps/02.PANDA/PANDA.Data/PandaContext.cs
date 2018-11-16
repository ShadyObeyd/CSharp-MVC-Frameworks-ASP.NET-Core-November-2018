using Microsoft.EntityFrameworkCore;
using PANDA.Models;

namespace PANDA.Data
{
    public class PandaContext : DbContext
    {
        public PandaContext(DbContextOptions options) 
            : base(options)
        {

        }

        public PandaContext()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Receipt)
                .WithOne(r => r.Package)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
