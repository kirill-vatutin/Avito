using Avito.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Avito.Infrastructure
{
    public class AvitoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<WishlistItem> WishLists { get; set; } = null!;

        public AvitoDbContext(DbContextOptions<AvitoDbContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<User>().HasMany(k => k.Products);



            modelBuilder.Entity<User>()
              .HasMany(u => u.Products)
              .WithOne(p => p.User)
              .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
             .HasMany(u => u.WishList)
             .WithOne(p => p.User)
             .HasForeignKey(p => p.UserId);






            modelBuilder.Entity<Product>().HasKey(k => k.Id);
            modelBuilder.Entity<Role>().HasKey(k => k.Id);
            modelBuilder.Entity<Category>().HasKey(k => k.Id);

        }

    }
}
