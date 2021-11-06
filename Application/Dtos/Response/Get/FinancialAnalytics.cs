namespace Application.Dtos.Response.Get
{
    public class FinancialAnalytics
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
    }
    
    public class FinancialAnalyticsDto
    {
        public int Label { get; set; }
        public decimal Value { get; set; }
    }
}