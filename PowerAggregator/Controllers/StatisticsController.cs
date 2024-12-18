using Microsoft.AspNetCore.Mvc;
using PowerAggregator.DAL;
using PowerAggregator.Data;
using PowerAggregator.Models;

namespace PowerAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticRepository statisticRepository;
        public StatisticsController(PowerAggregationContext context)
        {
            statisticRepository = new StatisticRepository(context);
        }
        // GET: api/Statistics
        [HttpGet]
        public IActionResult GetStatistics()
        {
            IEnumerable<MonthlyRegionStatistic> statistics = statisticRepository.GetStatistics();
            return statistics == null ? NotFound() : Ok(statistics);
        }
    }
}
