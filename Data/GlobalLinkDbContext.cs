using Microsoft.EntityFrameworkCore;
using GlobalLinkAPI.Models;

namespace GlobalLinkAPI.Data
{
    public class GlobalLinkDbContext : DbContext
    {
        public GlobalLinkDbContext(DbContextOptions<GlobalLinkDbContext> options)
            : base(options)
        {
        }
        public DbSet<Ong> Ongs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Donate> Donations { get; set; }
        public DbSet<Need> Needs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ong>().ToTable("Ongs");
            builder.Entity<Company>().ToTable("Companies");
            builder.Entity<Donate>().ToTable("Donations");
            builder.Entity<Need>().ToTable("Needs");
        }
    }
}
