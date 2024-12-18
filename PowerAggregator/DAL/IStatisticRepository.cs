using PowerAggregator.Models;

namespace PowerAggregator.DAL
{
    public interface IStatisticRepository : IDisposable
    {
        IEnumerable<MonthlyRegionStatistic> GetStatistics();
        MonthlyRegionStatistic GetStatisticById(int id);
        IEnumerable<MonthlyRegionStatistic> GetStatisticsByRegion(string region);
        IEnumerable<MonthlyRegionStatistic> GetStatisticsByDate(DateTime yearMonth);
        void InsertStatistic(MonthlyRegionStatistic statistic);
        void DeleteStatistic(int id);
        void DeleteAllStatistics();
        void UpdateStatistic(MonthlyRegionStatistic statistic);
        void Save();
    }
}
