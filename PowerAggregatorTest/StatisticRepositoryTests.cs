using Microsoft.EntityFrameworkCore;
using PowerAggregator.DAL;
using PowerAggregator.Data;
using PowerAggregator.Models;

namespace PowerAggregatorTest
{
    public class StatisticRepositoryTests
    {
        private PowerAggregationContext context;

        public StatisticRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<PowerAggregationContext>().
                UseInMemoryDatabase
                (
                    databaseName: Guid.NewGuid().ToString()
                );
            context = new PowerAggregationContext(dbOptions.Options);
        }
            
        [Fact]
        public void InsertStatistic()
        {
            var repo = new StatisticRepository(context);
            var statistic = new MonthlyRegionStatistic
            {
                RegionName = "test",
                MonthDate = DateTime.Now,
                ProducedPower = 0,
                ConsumedPower = 0,
            };
            repo.InsertStatistic(statistic);
            repo.Save();
            IEnumerable<MonthlyRegionStatistic> list = repo.GetStatistics();
            Assert.Single(list);
        }

        [Fact]
        public async Task GetAllStatistics()
        {
            var statistics = new List<MonthlyRegionStatistic>()
            {
                new MonthlyRegionStatistic() { RegionName = "test1" },
                new MonthlyRegionStatistic() { RegionName = "test2" },
                new MonthlyRegionStatistic() { RegionName = "test3" }
            };
            context.Statistics.AddRange(statistics);
            await context.SaveChangesAsync();

            var repo = new StatisticRepository(context);
            IEnumerable<MonthlyRegionStatistic> result = repo.GetStatistics();
            Assert.Equal(statistics.Count, result.Count());
        }

        [Fact]
        public async Task GetByRegion()
        {
            var statistics = new List<MonthlyRegionStatistic>()
            {
                new MonthlyRegionStatistic() { RegionName = "test1" },
                new MonthlyRegionStatistic() { RegionName = "test2" },
                new MonthlyRegionStatistic() { RegionName = "test3" }
            };
            context.Statistics.AddRange(statistics);
            await context.SaveChangesAsync();

            var repo = new StatisticRepository(context);
            Assert.NotEmpty(repo.GetStatisticsByRegion("test1"));
            Assert.Empty(repo.GetStatisticsByRegion("test4"));
        }

        [Fact]
        public async Task GetByDate()
        {
            var statistics = new List<MonthlyRegionStatistic>()
            {
                new MonthlyRegionStatistic() { RegionName = "test1", MonthDate = DateTime.Parse("2024-10") },
                new MonthlyRegionStatistic() { RegionName = "test1", MonthDate = DateTime.Parse("2024-11") },
                new MonthlyRegionStatistic() { RegionName = "test1", MonthDate = DateTime.Parse("2024-12") }
            };
            context.Statistics.AddRange(statistics);
            await context.SaveChangesAsync();

            var repo = new StatisticRepository(context);
            Assert.NotEmpty(repo.GetStatisticsByDate(DateTime.Parse("2024-10")));
            Assert.Empty(repo.GetStatisticsByDate(DateTime.Parse("2025-01")));
        }

        [Fact]
        public async Task DeleteAll()
        {
            var statistics = new List<MonthlyRegionStatistic>()
            {
                new MonthlyRegionStatistic() { RegionName = "test1" },
                new MonthlyRegionStatistic() { RegionName = "test2" },
                new MonthlyRegionStatistic() { RegionName = "test3" }
            };
            context.Statistics.AddRange(statistics);
            await context.SaveChangesAsync();

            var repo = new StatisticRepository(context);
            repo.DeleteAllStatistics();
            await context.SaveChangesAsync();
            Assert.Empty(context.Statistics);
        }
    }
}