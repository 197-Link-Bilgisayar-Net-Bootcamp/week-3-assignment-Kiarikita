using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;

namespace NLayer.Data
{
    public class AppDbContext : DbContext //uygulamada kullanacağımız veritabanına karşılık geliyor
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {


        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api
            //modelBuilder.Entity<Product>().Property(x => x.Stock).HasColumnName("Category");

            base.OnModelCreating(modelBuilder);
        }
    }

}
