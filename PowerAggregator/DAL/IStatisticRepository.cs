using PowerAggregator.Models;

namespace PowerAggregator.DAL
{
    public interface IStatisticRepository : IDisposable
    {
        IEnumerable<MonthlyRegionStatistic> GetStatistics();
        MonthlyRegionStatistic GetStatisticById(int id);
        void InsertStatistic(MonthlyRegionStatistic statistic);
        void DeleteStatistic(int id);
        void UpdateStatistic(MonthlyRegionStatistic statistic);
        void Save();
    }
}
