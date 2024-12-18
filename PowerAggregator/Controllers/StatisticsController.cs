﻿using Microsoft.AspNetCore.Mvc;
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
        private AggregationService aggregationService;
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

        // POST: api/Statistics
        [HttpPost]
        public IActionResult PostStatistcis(string url)
        {
            if (aggregationService.ProcessStatisticUrl(url))
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
