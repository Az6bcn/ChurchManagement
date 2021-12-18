namespace Application.Dtos.Response.Get;

public class GetDashboardDataResponseDto
{
    public int Members{ get; set; }
    public FinanceProgressInfo Tithe { get; set; }
    public FinanceProgressInfo Thanksgiving { get; set; }
    public FinanceProgressInfo Expenses { get; set; }
    public FinanceProgressInfo Offering { get; set; }
    public FinanceProgressInfo MidWeekServiceOffering { get; set; }
    public FinanceProgressInfo SpecialThanksgiving { get; set; }
    public IDictionary<string, GetAttendanceResponseDto>? CurrentMonthAttendance { get; set; }
    public IDictionary<DateTime, GetAttendanceResponseDto>? LastYearAttendance { get; set; }
    public IDictionary<string, GetAttendanceResponseDto>? CurrentYearAttendance { get; set; }
    public int NewComers { get; set; }
    public IEnumerable<GetMonthAttendanceSummaryResponseDto> MonthAttendanceSummary { get; set; }
    public IEnumerable<FinancialAnalytics> ExpensesFinancialAnalytics { get; set; }
    public IEnumerable<FinancialAnalytics> IncomeFinancialAnalytics { get; set; }
    public GetFinancialBreakdownInPercentage FinancialBreakdownInPercentage { get; set; }
    public string CurrencyCode { get; set; }
}