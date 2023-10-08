using api_demo_products.Models;
using Microsoft.EntityFrameworkCore;

namespace api_demo_products.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        IConfiguration appConfig;

        public ProductContext(IConfiguration config)
        {
            appConfig = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Products;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(appConfig.GetConnectionString("AzureSQL"));
        }
    }
}
