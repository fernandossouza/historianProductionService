using Microsoft.EntityFrameworkCore;
using historianproductionservice.Model;
using historianproductionservice.Model.Genealogy;

namespace historianproductionservice.Data{
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Genealogy> Genealogy { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductTraceability> ProductTraceabilities { get; set; }
        public DbSet<EndRoll> EndRoll { get; set; }
        public DbSet<Aco> Aco { get; set; }        
        public DbSet<Liga> Liga { get; set; }
        public DbSet<Tool> Tool { get; set; }
        public DbSet<Elemento> Elemento { get; set; }
    }
}