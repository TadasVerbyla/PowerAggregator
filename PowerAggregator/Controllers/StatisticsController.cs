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
        private ILogger<StatisticsController> logger;

        public StatisticsController(PowerAggregationContext context, ILogger<StatisticsController> logger)
        {
            statisticRepository = new StatisticRepository(context);
            aggregationService = new AggregationService(statisticRepository);
            this.logger = logger;
        }

        // GET: api/Statistics
        [HttpGet]
        public IActionResult GetStatistics()
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatistics();
            logger.LogInformation($"All statistics requested at: {DateTime.Now}");
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // Get: api/Statistics/5
        [HttpGet("{region}")]
        public IActionResult GetStatisticsByRegion(string region)
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatisticsByRegion(region);
            logger.LogInformation($"Statistics for {region} region requested at: {DateTime.Now}");
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // Get: api/Statistics/5
        [HttpGet("{yearMonth:datetime}")]
        public IActionResult GetStatisticsByDate(DateTime yearMonth)
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatisticsByDate(yearMonth);
            logger.LogInformation($"Statistics for the month of {yearMonth} requested at: {DateTime.Now}");
            return statistics == null ? NotFound() : Ok(statistics);
        }

        // POST: api/Statistics
        [HttpPost]
        public IActionResult PostStatistcis(string url)
        {
            if (aggregationService.ProcessStatisticURL(url))
            {
                logger.LogInformation($"AggregationService successfully aggregated {url} statistics at {DateTime.Now}");
                return Ok(new { message = "CSV successfully aggregated. "});
            }
            else
            {
                logger.LogError($"AggregationService failed to aggregate {url} statistics at {DateTime.Now}");
                return StatusCode(500, new { message = "Failed to aggregate CSV. " });
            }
        }

        // DELETE: api/Statistics
        [HttpDelete]
        public IActionResult DeleteAllStatistcis()
        {
            statisticRepository.DeleteAllStatistics();
            statisticRepository.Save();
            logger.LogCritical($"All statistics were deleted at {DateTime.Now}");
            return Ok();
        }
    }
}
