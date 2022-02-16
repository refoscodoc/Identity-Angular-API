using MariaDb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MariaDb.API.DataAccess;

public class MariaDbDataAccess : DbContext
{
    public MariaDbDataAccess(DbContextOptions<MariaDbDataAccess> options) : base(options)
    { }
    
    public DbSet<UserModel> UserTable { get; set; }
    public DbSet<ProductModel> ProductTable { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserModel>().HasKey(m => m.UserId).HasName("PrimaryKey_UserId");
        builder.Entity<ProductModel>().HasKey(m => m.ProductId).HasName("PrimaryKey_ProductId");

        base.OnModelCreating(builder);
    }
    
    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
            
        UpdateUpdatedProperty<ProductModel>();


        return base.SaveChanges();
    }
    
    private void UpdateUpdatedProperty<T>() where T : class
    {
        var modifiedSourceInfo =
            ChangeTracker.Entries<T>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in modifiedSourceInfo)
        {
            entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
        }
    }
}