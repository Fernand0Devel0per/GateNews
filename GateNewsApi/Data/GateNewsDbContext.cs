using GateNewsApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.Data
{
    public class GateNewsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public GateNewsDbContext(DbContextOptions<GateNewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
               .HasMany(u => u.News)
               .WithOne(n => n.Author)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.User)
                .WithOne(u => u.Author)
                .HasForeignKey<Author>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<News>()
                    .HasOne(n => n.Author)
                    .WithMany(a => a.News)
                    .HasForeignKey(n => n.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
