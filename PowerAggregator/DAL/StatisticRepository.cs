using Microsoft.EntityFrameworkCore;
using PowerAggregator.Data;
using PowerAggregator.Models;
using System.Diagnostics;

namespace PowerAggregator.DAL
{
    public class StatisticRepository : IStatisticRepository, IDisposable
    {
        private PowerAggregationContext context;

        public StatisticRepository(PowerAggregationContext context)
        {
            this.context = context;
        }
        public void InsertStatistic(MonthlyRegionStatistic statistic)
        {
            context.Statistics.Add(statistic);
        }

        public IEnumerable<MonthlyRegionStatistic> GetStatistics()
        {
            return context.Statistics.ToList();
        }

        public IEnumerable<MonthlyRegionStatistic> GetStatisticsByRegion(string region)
        {
            return context.Statistics.ToList()
                .Where(statistic => statistic.RegionName == region);
        }

        public IEnumerable<MonthlyRegionStatistic> GetStatisticsByDate(DateTime yearMonth)
        {
            return context.Statistics.ToList()
                .Where(statistic => statistic.MonthDate == yearMonth);
        }
        public void DeleteAllStatistics()
        {
            context.Statistics.RemoveRange(GetStatistics());
        }
        public void Save()
        {
            context.SaveChanges();
        }

        //public void DeleteStatistic(int id)
        //{
        //    MonthlyRegionStatistic? statistic = context.Statistics.Find(id);
        //    if (statistic != null)
        //    {
        //        context.Statistics.Remove(statistic);
        //    }
        //}

        //public MonthlyRegionStatistic GetStatisticById(int id)
        //{
        //    return context.Statistics.Find(id);
        //}

        //public void UpdateStatistic(MonthlyRegionStatistic newStatistic)
        //{
        //    MonthlyRegionStatistic? statistic = context.Statistics.Find(newStatistic.Id);
        //    if (statistic != null)
        //    {
        //        statistic.RegionName = newStatistic.RegionName;
        //        statistic.ProducedPower = newStatistic.ProducedPower;
        //        statistic.ConsumedPower = newStatistic.ConsumedPower;
        //        statistic.MonthDate = newStatistic.MonthDate;
        //        context.Entry(statistic).State = EntityState.Modified;
        //    }
        //}

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
