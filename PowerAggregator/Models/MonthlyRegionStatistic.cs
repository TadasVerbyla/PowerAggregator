using System.ComponentModel.DataAnnotations;

namespace PowerAggregator.Models
{
    public class MonthlyRegionStatistic
    {
        public int Id { get; set; }
        [Required]
        public string? RegionName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime MonthDate { get; set; }
        [Required]
        public decimal ConsumedPower { get; set; }
        [Required]
        public decimal ProducedPower { get; set; }

    }
}
