using Demo.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DB.Entities;

namespace Demo.Core.Context;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options): base(options) {}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"data source=DUNGEONLAPTOP\Sabir;Database=DemoPractice;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True")
            .UseCamelCaseNamingConvention();
    }
    
    public DbSet<Material> Materials { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
}