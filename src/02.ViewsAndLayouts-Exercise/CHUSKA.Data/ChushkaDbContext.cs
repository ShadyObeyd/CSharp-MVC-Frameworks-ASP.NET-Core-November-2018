namespace CHUSKA.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ChuskaDbContext : IdentityDbContext<ChushkaUser>
    {
        public ChuskaDbContext(DbContextOptions<ChuskaDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Chushka;Trusted_Connection=True;MultipleActiveResultSets=true");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
