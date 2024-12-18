using Microsoft.AspNetCore.Mvc;
using PowerAggregator.DAL;
using PowerAggregator.Data;
using PowerAggregator.Models;
using PowerAggregator.Services;

namespace PowerAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticRepository statisticRepository;
        private IAggregationService aggregationService;
        public StatisticsController(PowerAggregationContext context)
        {
            statisticRepository = new StatisticRepository(context);
            aggregationService = new AggregationService(statisticRepository);
        }

        // GET: api/Statistics
        [HttpGet]
        public IActionResult GetStatistics()
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatistics();
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // Get: api/Statistics/5
        [HttpGet("{region}")]
        public IActionResult GetStatisticsByRegion(string region)
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatisticsByRegion(region);
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // Get: api/Statistics/5
        [HttpGet("{yearMonth:datetime}")]
        public IActionResult GetStatisticsByDate(DateTime yearMonth)
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatisticsByDate(yearMonth);
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // POST: api/Statistics
        [HttpPost]
        public IActionResult PostStatistcis(string url)
        {
            if (aggregationService.ProcessStatisticURL(url))
            {
                return Ok(new { message = "CSV successfully aggregated. "});
            }
            else
            {
                return StatusCode(500, new { message = "Failed to aggregate CSV. " });
            }
        }
    }
}
