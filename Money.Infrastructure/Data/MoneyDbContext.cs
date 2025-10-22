using Microsoft.EntityFrameworkCore;
using Money.Domain.Models;

namespace Money.Infrastructure.Data;

public partial class MoneyDbContext : DbContext
{
    public MoneyDbContext(DbContextOptions<MoneyDbContext> options) : base(options)
    {
    }

    public MoneyDbContext()
    {
    }

    public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure CurrencyConversion entity
        modelBuilder.Entity<CurrencyConversion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FromCurrency).IsRequired().HasMaxLength(3);
            entity.Property(e => e.ToCurrency).IsRequired().HasMaxLength(3);
            entity.Property(e => e.Amount).IsRequired().HasConversion<decimal>().HasPrecision(12, 2);
            entity.Property(e => e.ConvertedAmount).IsRequired().HasConversion<decimal>().HasPrecision(12, 2);
            entity.Property(e => e.ExchangeRate).IsRequired().HasConversion<decimal>().HasPrecision(12, 6);
            entity.Property(e => e.DepartmentId).IsRequired().HasMaxLength(100);

        });
    }
}
