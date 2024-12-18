using Microsoft.IdentityModel.Tokens;
using PowerAggregator.DAL;
using PowerAggregator.Data;
using PowerAggregator.Models;
using System.Diagnostics;
using System.Net;

namespace PowerAggregator.Services
{
    public class AggregationService : IAggregationService
    {
        private IStatisticRepository statisticRepository;
        public AggregationService(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }
        public bool ProcessStatisticURL(string url)
        {
            try
            {
                string csv = DownloadCSV(url).Result;
                var records = ParseCSV(csv);
                var apartments = FilterByObjectName(records, "BUTAS");
                var statistics = AggregateRegionStatistics(apartments);
                foreach (var region in statistics)
                {
                    statisticRepository.InsertStatistic(region);
                }
                statisticRepository.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private List<MonthlyRegionStatistic> AggregateRegionStatistics(List<PowerUsageRecord> apartmentRecords)
        {
            return apartmentRecords
                .GroupBy(record => new { record.RegionName, YearMonth = new DateTime(record.Date.Year, record.Date.Month, 1) })
                .Select(statistic => new MonthlyRegionStatistic
                {
                    RegionName = statistic.Key.RegionName,
                    MonthDate = statistic.Key.YearMonth,
                    ConsumedPower = statistic.Sum(record => record.ConsumedPower),
                    ProducedPower = statistic.Sum(record => record.ProducedPower),
                })
                .ToList();
        }

        private List<PowerUsageRecord> FilterByObjectName(List<PowerUsageRecord> records, string objectName)
        {
            return records.Where(record => record.ObjectName == objectName).ToList();
        }

        private List<PowerUsageRecord> ParseCSV(string csv)
        {
            var rows = csv.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var records = new List<PowerUsageRecord>();

            foreach (var row in rows.Skip(1))
            {
                var elements = row.Split(';');

                var regionName = elements[0];
                var objectName = elements[1];
                var objectType = elements[2];
                var number = int.Parse(elements[3]);
                var powerConsumed = decimal.Parse(elements[4]);
                var date = DateTime.Parse(elements[5]);
                var powerProduced = 0m;
                if (!string.IsNullOrEmpty(elements[6]))
                {
                    powerProduced = decimal.Parse(elements[6]);
                }

                var record = new PowerUsageRecord(
                    regionName,
                    objectName,
                    objectType,
                    number,
                    powerConsumed,
                    date,
                    powerProduced
                );
                records.Add(record);
            }
            return records;
        }

        private async Task<string> DownloadCSV(string url)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new Exception($"Failed to download {url}");  
        }
    }
}
