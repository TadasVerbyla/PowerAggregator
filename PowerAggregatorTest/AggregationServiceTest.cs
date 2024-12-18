using Microsoft.EntityFrameworkCore;
using PowerAggregator.DAL;
using PowerAggregator.Data;
using PowerAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PowerAggregatorTest
{
    public class AggregationServiceTest
    {
        private PowerAggregationContext context;
        private IAggregationService aggregationService;

        public AggregationServiceTest()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<PowerAggregationContext>().
                UseInMemoryDatabase
                (
                    databaseName: Guid.NewGuid().ToString()
                );
            context = new PowerAggregationContext(dbOptions.Options);
            aggregationService = new AggregationService(new StatisticRepository(context));
        }

        [Fact]
        public void ProcessStatistics()
        {
            var url = "https://data.gov.lt/media/filer_public/be/39/be390ff0-8972-474e-a044-9f6f6f5f589a/2024-09.csv";
            Assert.True(aggregationService.ProcessStatisticURL(url));
            Assert.NotEmpty(context.Statistics.ToList());
        }
    }
}
