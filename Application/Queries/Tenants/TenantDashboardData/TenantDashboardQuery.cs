using System.Globalization;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities.AttendanceAggregate;
using Shared.Enums;

namespace Application.Queries.Tenants.TenantDashboardData;

public class TenantDashboardQuery : IQueryTenantDashboardData
{
    private readonly ITenantRepositoryAsync _tenantRepo;
    private readonly IFinanceRepositoryAsync _financeRepo;
    private readonly IAttendanceRepositoryAsync _attendanceRepo;
    private readonly IPersonManagementRepositoryAsync _personMangeRpo;

    public TenantDashboardQuery(ITenantRepositoryAsync tenantRepo,
                                IFinanceRepositoryAsync financeRepo,
                                IAttendanceRepositoryAsync attendanceRepo,
                                IPersonManagementRepositoryAsync personMangeRpo)
    {
        _tenantRepo = tenantRepo;
        _financeRepo = financeRepo;
        _attendanceRepo = attendanceRepo;
        _personMangeRpo = personMangeRpo;
    }

    public async Task<QueryResult<GetDashboardDataResponseDto>> ExecuteAsync(int tenantId,
                                                                             string currencyCode,
                                                                             DateTime? startDate,
                                                                             DateTime? endDate)
    {
        var today = DateTime.UtcNow;
        DateOnly lastYearStartDate = startDate.HasValue ? DateOnly.FromDateTime(startDate.Value) : default;
        DateOnly currentMonthEndDate = endDate.HasValue ? DateOnly.FromDateTime(endDate.Value) : default;
        DateOnly currentMonthStartDate = default;
        DateOnly lastMonthStartDate = default;

        if (!startDate.HasValue || !endDate.HasValue)
        {
            currentMonthStartDate = GetDate(today.Year, today.Month, 1);
            lastYearStartDate = GetDate(today.Year - 1, today.Month, 1);
            currentMonthEndDate = GetDate(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            lastMonthStartDate = GetDate(today.Year, today.Month - 1, 1);
        }

        var personManagement 
            = await _personMangeRpo.GetPersonsBetweenDatesByTenantIdAsync(tenantId);

        var finances
            = await _financeRepo.GetFinancesBetweenDatesByTenantIdAsync(tenantId,
                                                                        lastYearStartDate,
                                                                        currentMonthEndDate);

        // var attendances 
        //     = await _attendanceRepo.GetAttendancesBetweenDatesByTenantIdAsync(tenantId,
        //                                                                       _startDate,
        //                                                                       _endDate);

        var attendances
            = await _attendanceRepo.GetAttendancesBetweenDatesByTenantIdAsync(tenantId,
                                                                              lastYearStartDate,
                                                                              currentMonthEndDate);

        var allAttendancesGroupedByDate = attendances
                                          .GroupBy(x => x.ServiceDate)
                                          .ToDictionary(x => x.Key,
                                                        x => new GetAttendanceResponseDto(x.ToList()));

        var lastYearAttendances
            = ExtractLastYearAttendances(allAttendancesGroupedByDate, currentMonthEndDate);

        var currentYearAttendances
            = ExtractCurrentYearAttendances(allAttendancesGroupedByDate, lastYearAttendances);

        var currentMonthAttendances
            = ExtractCurrentMonthAttendances(currentYearAttendances,
                                             currentMonthStartDate,
                                             currentMonthEndDate);

        var lastCurrentFinances = finances
            .Where(x => DateOnly.FromDateTime(x.GivenDate.Date) >= lastMonthStartDate
                        && DateOnly.FromDateTime(x.GivenDate.Date) <= currentMonthEndDate);

        var dashboardData = MapDashboardData(personManagement.members,
                                             personManagement.newComers,
                                             currencyCode,
                                             lastCurrentFinances.ToList(),
                                             currentMonthStartDate,
                                             lastMonthStartDate,
                                             finances.ToList());

        dashboardData.CurrentMonthAttendance = currentMonthAttendances
                                               .OrderBy(x => x.Key)
                                               .ToDictionary(x => x.Key.ToString("dd/MM/yyyy"),
                                                             x => x.Value);
        dashboardData.CurrentYearAttendance = currentYearAttendances
                                              .OrderBy(x => x.Key)
                                              .ToDictionary(x => x.Key.ToString("dd/MM/yyyy"),
                                                            x => x.Value);
        dashboardData.LastYearAttendance = lastYearAttendances;
        dashboardData.MonthAttendanceSummary = ExtractMonthAttendanceSummary(currentMonthAttendances);

        var incomeFinancialAnalytics =
            ProcessAllYearFinancialData(finances.ToList().Where(x => x.FinanceTypeId != (int)FinanceEnum.Spending).ToList())
                .ToList();
        var incomeFinancialAnalyticsOrdered = incomeFinancialAnalytics
                                              .OrderBy(x => x.Label)
                                              .Select(x => new FinancialAnalytics()
                                              {
                                                  Label = DateTimeFormatInfo.GetInstance(CultureInfo.InvariantCulture).GetMonthName(x.Label),
                                                  Value = x.Value
                                              });
        var expensesFinancialAnalytics =
            ProcessAllYearFinancialData(finances.ToList().Where(x => x.FinanceTypeId == (int)FinanceEnum.Spending).ToList())
                .ToList();


        var expensesPadded
            = PadExpensesFinancialAnalytic(incomeFinancialAnalytics.ToList(), expensesFinancialAnalytics.ToList());

        var expensesFinancialAnalyticsOrdered = expensesPadded
                                                .OrderBy(x => x.Label)
                                                .Select(x => new FinancialAnalytics()
                                                {
                                                    Label = DateTimeFormatInfo.GetInstance(CultureInfo.InvariantCulture).GetMonthName(x.Label),
                                                    Value = x.Value
                                                });

        dashboardData.ExpensesFinancialAnalytics = expensesFinancialAnalyticsOrdered;
        dashboardData.IncomeFinancialAnalytics = incomeFinancialAnalyticsOrdered;

        var financialPercentageCalc = new GetFinancialBreakdownInPercentage()
        {
            ExpensesTotal = finances.Sum(x => x.Amount),
            IncomePercentage = CalculatePercentage(
                                                   finances.Sum(x => x.Amount),
                                                   incomeFinancialAnalytics.Sum(x => x.Value)
                                                  ),
            Expenses = expensesFinancialAnalytics.Sum(x => x.Value),
            ExpensesPercentage = CalculatePercentage(
                                                     finances.Sum(x => x.Amount),
                                                     expensesFinancialAnalytics.Sum(x => x.Value)
                                                    )
        };

        dashboardData.FinancialBreakdownInPercentage = financialPercentageCalc;

        var response
            = QueryResult<GetDashboardDataResponseDto>.CreateQueryResult(dashboardData);

        return response;
    }

    private Dictionary<DateTime, GetAttendanceResponseDto>? ExtractCurrentMonthAttendances(Dictionary<DateTime, GetAttendanceResponseDto>? currentYearAttendances,
        DateOnly currentMonthStartDate,
        DateOnly currentMonthEndDate)
        => currentYearAttendances.Where(x => DateOnly.FromDateTime(x.Key) >= currentMonthStartDate
                                             && DateOnly.FromDateTime(x.Key) <= currentMonthEndDate)
                                 .ToDictionary(x => x.Key,
                                               x => x.Value);

    private Dictionary<DateTime, GetAttendanceResponseDto>? ExtractLastYearAttendances(Dictionary<DateTime, GetAttendanceResponseDto>? allAttendancesGroupedByDate,
        DateOnly currentDate)
        => allAttendancesGroupedByDate.Where(x => x.Key.Year == currentDate.AddYears(-1).Year)
                                      .ToDictionary(x => x.Key,
                                                    x => x.Value);

    private Dictionary<DateTime, GetAttendanceResponseDto>? ExtractCurrentYearAttendances(Dictionary<DateTime, GetAttendanceResponseDto>? allAttendancesGroupedByDate,
        Dictionary<DateTime, GetAttendanceResponseDto>? lastYearAttendances)
        => allAttendancesGroupedByDate.ExceptBy(lastYearAttendances.Select(x => x.Key),
                                                x => x.Key)
                                      .ToDictionary(x => x.Key,
                                                    x => x.Value);

    private IEnumerable<GetMonthAttendanceSummaryResponseDto>
        ExtractMonthAttendanceSummary(Dictionary<DateTime, GetAttendanceResponseDto>? currentMonthAttendances)
    {
        var response = new List<GetMonthAttendanceSummaryResponseDto>();
        var list = currentMonthAttendances.Values.ToList();
        var totalMen = new GetMonthAttendanceSummaryResponseDto
        {
            Title = "Men",
            Value = list.Sum(x => x.Men)
        };
        var totalWomen = new GetMonthAttendanceSummaryResponseDto
        {
            Title = "Women",
            Value = list.Sum(x => x.Women)
        };
        var totalChildren = new GetMonthAttendanceSummaryResponseDto
        {
            Title = "Children",
            Value = list.Sum(x => x.Children)
        };
        var total = new GetMonthAttendanceSummaryResponseDto
        {
            Title = "Total",
            Value = list.Sum(x => x.Total)
        };

        response.Add(totalMen);
        response.Add(totalWomen);
        response.Add(totalChildren);
        response.Add(total);

        return response;
    }

    private GetDashboardDataResponseDto MapDashboardData(int members,
                                                         int newComers,
                                                         string currencyCode,
                                                         ICollection<Domain.Entities.FinanceAggregate.Finance> finances,
                                                         DateOnly current,
                                                         DateOnly last,
                                                         ICollection<Domain.Entities.FinanceAggregate.Finance>
                                                             allYearFinances)
    {
        return new GetDashboardDataResponseDto
        {
            Members = members,
            Tithe = GetFinancialProgress(finances,
                                         (int)FinanceEnum.Tithe,
                                         current,
                                         last,
                                         "Tithe"),
            Thanksgiving = GetFinancialProgress(finances,
                                                (int)FinanceEnum.Thanksgiving,
                                                current,
                                                last,
                                                "Thanksgiving"),
            Expenses = GetFinancialProgress(finances,
                                            (int)FinanceEnum.Spending,
                                            current,
                                            last,
                                            "Expenses"),
            Offering = GetFinancialProgress(finances,
                                            (int)FinanceEnum.Offering,
                                            current,
                                            last,
                                            "Offering"),
            MidWeekServiceOffering = GetFinancialProgress(finances,
                                                          (int)FinanceEnum.MidWeekServiceOffering,
                                                          current,
                                                          last,
                                                          "Midweek Offering"),
            SpecialThanksgiving = GetFinancialProgress(finances,
                                                       (int)FinanceEnum.SpecialThanksgiving,
                                                       current,
                                                       last,
                                                       "Special Thanksgiving"),
            NewComers = newComers,
            CurrencyCode = currencyCode
        };
    }

    private IEnumerable<FinancialAnalyticsDto> ProcessAllYearFinancialData(ICollection<Domain.Entities.FinanceAggregate.Finance> finances)
    {
        var groupedFinanceByMonth = finances.GroupBy(x => x.GivenDate.Month);

        foreach (var group in groupedFinanceByMonth)
        {
            yield return new()
            {
                Label = group.Key,
                Value = group.Sum(x => x.Amount)
            };
        }
    }


    private FinanceProgressInfo GetFinancialProgress(ICollection<Domain.Entities.FinanceAggregate.Finance> finances,
                                                     int financeTypeId,
                                                     DateOnly current,
                                                     DateOnly last,
                                                     string title)
    {
        var data = finances.Where(x => x.FinanceTypeId == financeTypeId);
        //var groupedData = data.GroupBy(x => x.GivenDate.Date.Month);
        var currentMonthSum = data
                              .Where(x => x.GivenDate.Date.Month == current.Month)
                              .Sum(x => x.Amount);

        var lastMonthSum = data
                           .Where(x => x.GivenDate.Date.Month == last.Month)
                           .Sum(x => x.Amount);
        var percentageIncrease = CalculateProgress(currentMonthSum, lastMonthSum);

        return new()
        {
            Title = title,
            Value = currentMonthSum,
            ActiveProgress = percentageIncrease,
            Description = percentageIncrease < 0m
                              ? $"({percentageIncrease}% less)"
                              : $"({percentageIncrease}% more)"
        };
    }

    private decimal CalculateProgress(decimal currentMonthValue,
                                      decimal lastMonthValue)
    {
        if (lastMonthValue == 0)
            return default;

        var percentageIncrease = ((currentMonthValue - lastMonthValue) / lastMonthValue) * 100;
        return Math.Round(percentageIncrease, 2 , MidpointRounding.AwayFromZero);
    }

    private IEnumerable<FinancialAnalyticsDto> PadExpensesFinancialAnalytic(List<FinancialAnalyticsDto> income,
                                                                            List<FinancialAnalyticsDto> expenses)
    {
        var missing = income
                      .ExceptBy(expenses.Select(x => x.Label), x => x.Label)
                      .ToList()
                      .Select(x => new FinancialAnalyticsDto()
                      {
                          Label = x.Label,
                          Value = 0
                      });

        expenses.AddRange(missing);
        return expenses.OrderBy(x => x.Label).ToList();
    }

    private decimal CalculatePercentage(decimal total,
                                        decimal num)
    {
        var result = (num / total) * 100;

        return Math.Round(result, 2, MidpointRounding.AwayFromZero);
    }

    private DateOnly GetDate(int year, int month, int day) => new DateOnly(year, month, day);

    private IDictionary<DateTime, GetAttendanceResponseDto> Get(IEnumerable<Attendance>? attendances) => default;
}