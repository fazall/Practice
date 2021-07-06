using ErrorApp.Data.Configuration;
using ErrorApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorApp.Data.Context
{
    public class ProductDbContext : DbContext 
    {
       public DbSet<Product> products { get; set; }


        public ProductDbContext()
        {

        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.EnableSensitiveDataLogging(true);
                //Postgresql
                //options.UseNpgsql
                options.UseSqlServer("Server=.;Database=ProductDbContext;Trusted_Connection=True;MultipleActiveResultSets=true",
                    (opts) =>
                {

                });
            }
            base.OnConfiguring(options);
        }

    }
}
