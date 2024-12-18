namespace PowerAggregator.Models
{
    public struct PowerUsageRecord
    {
        public string RegionName { get; }
        public string ObjectName { get; }
        public string ObjectType { get; }
        public int Number { get; }
        public decimal ConsumedPower { get; }
        public DateTime Date { get; }
        public decimal ProducedPower { get; }

        public PowerUsageRecord(string regionName, string objectName, string objectType, int number, decimal consumedPower, DateTime date, decimal producedPower)
        {
            RegionName = regionName;
            ObjectName = objectName;
            ObjectType = objectType;
            Number = number;
            ConsumedPower = consumedPower;
            Date = date;
            ProducedPower = producedPower;
        }
    }
}
