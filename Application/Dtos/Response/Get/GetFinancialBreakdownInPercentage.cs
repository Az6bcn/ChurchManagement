namespace Application.Dtos.Response.Get
{
    public class GetFinancialBreakdownInPercentage
    {
        public decimal IncomePercentage { get; set; }
        public decimal ExpensesPercentage { get; set; }
        public decimal Expenses { get; set; }
        public decimal ExpensesTotal { get; set; }
    }
}