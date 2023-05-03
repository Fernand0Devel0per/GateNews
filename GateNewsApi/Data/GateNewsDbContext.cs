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
        public DbSet<Author> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
               .HasMany(u => u.News)
               .WithOne(n => n.Author)
               .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
