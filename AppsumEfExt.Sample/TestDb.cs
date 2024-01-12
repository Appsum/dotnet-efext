using Microsoft.EntityFrameworkCore;

namespace AppsumEfExt.Sample;

public class TestDb : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=test.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TestEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id);
        });
    }
}
