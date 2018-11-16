using FDMC.Models;
using Microsoft.EntityFrameworkCore;

namespace FDMC.Data
{
    public class FdmcContext : DbContext
    {
        public FdmcContext(DbContextOptions options) : base(options)
        {

        }

        public FdmcContext()
        {

        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
    }
}
