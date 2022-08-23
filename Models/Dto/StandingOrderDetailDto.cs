using System;


namespace StandingOrders.API.Models
{
    public class StandingOrderDetailDto
    {
        public int? StandingOrderId { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string VariableSymbol { get; set; }
        public string SpecificSymbol { get; set; }
        public string ConstantSymbol { get; set; }
        public string Note { get; set; }
        public int IntervalId { get; set; }
        public int IntervalSpecification { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}

