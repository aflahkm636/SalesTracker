using Microsoft.EntityFrameworkCore;
using SalesTracker.models;

namespace SalesTracker.data
{
    public class AppdbContext:DbContext
    {
        public AppdbContext (DbContextOptions<AppdbContext> options) : base(options)
        {

        }

        public DbSet<Product>products { get; set; }
        public DbSet<Sale> sale { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.product)
                .WithMany(p => p.Sale)
                .HasForeignKey(s => s.productId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

               modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 50000 },
            new Product { Id = 2, Name = "Mobile", Price = 20000 },
            new Product { Id = 3, Name = "Headphone", Price = 2500 }
        );
        }
    }
}
