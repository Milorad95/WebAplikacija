using Entity_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set;} 
        public DbSet<Product> Product { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Product>();


            modelBuilder.Entity<Category>()
                 .HasMany(c => c.Products)
                 .WithOne(e => e.Category);
                
        }

    }
}
