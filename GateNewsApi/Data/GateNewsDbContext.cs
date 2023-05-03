using GateNewsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.Data
{
    public class GateNewsDbContext : DbContext
    {
        public GateNewsDbContext(DbContextOptions<GateNewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(u => u.News)
               .WithOne(n => n.Author)
               .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
