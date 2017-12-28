using Microsoft.EntityFrameworkCore;
using historianproductionservice.Model;

namespace historianproductionservice.Data
{
    public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Order> ToolTypes{get;set;}
        public DbSet<Product> Tools{get;set;}
    }
}