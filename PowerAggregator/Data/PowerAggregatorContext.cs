using Microsoft.EntityFrameworkCore;
using PowerAggregator.Models;

namespace PowerAggregator.Data
{
    public class PowerAggregationContext : DbContext
    {
        public PowerAggregationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MonthlyRegionStatistic> Statistics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MonthlyRegionStatistic>()
                .HasIndex(x => new { x.RegionName, x.MonthDate })
                .IsUnique();
        }
    }
}
