using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Shared.Enums;

namespace Application.Queries.Tenant.TenantDashboardData
{
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
                                                                                 DateTime? startDate,
                                                                                 DateTime? endDate)
        {
            var today = DateTime.UtcNow;
            DateOnly currentMonthStartDate = startDate.HasValue ? DateOnly.FromDateTime(startDate.Value) : default;
            DateOnly currentMonthEndDate = endDate.HasValue ? DateOnly.FromDateTime(endDate.Value) : default;

            if (!startDate.HasValue || !endDate.HasValue)
            {
                currentMonthStartDate = new DateOnly(today.Year,
                                                     today.Month,
                                                     1);
                currentMonthEndDate = new DateOnly(today.Year,
                                                   today.Month,
                                                   DateTime.DaysInMonth(today.Year, today.Month));
            }

            var personManagement = await _personMangeRpo.GetPersonsBetweenDatesByTenantIdAsync(tenantId);

            var finances
                = await _financeRepo.GetFinancesBetweenDatesByTenantIdAsync(tenantId,
                                                                            currentMonthStartDate,
                                                                            currentMonthEndDate);

            // var attendances 
            //     = await _attendanceRepo.GetAttendancesBetweenDatesByTenantIdAsync(tenantId,
            //                                                                       _startDate,
            //                                                                       _endDate);
            var lastYearStartDate = new DateOnly(today.Year,
                                                 1,
                                                 1);
            var currentDate = new DateOnly(today.Year,
                                           today.Month,
                                           DateTime.DaysInMonth(today.Year, today.Month));
            var attendances
                = await _attendanceRepo.GetAttendancesBetweenDatesByTenantIdAsync(tenantId,
                                                                                  lastYearStartDate,
                                                                                  currentDate);

            var allAttendancesGroupedByDate = attendances
                                              .GroupBy(x => x.ServiceDate)
                                              .ToDictionary(x => x.Key,
                                                            x => new GetAttendanceResponseDto(x.ToList()));

            var lastYearAttendances
                = ExtractLastYearAttendances(allAttendancesGroupedByDate, currentDate);

            var currentYearAttendances
                = ExtractCurrentYearAttendances(allAttendancesGroupedByDate, lastYearAttendances);

            var currentMonthAttendances
                = ExtractCurrentMonthAttendances(currentYearAttendances,
                                                 currentMonthStartDate,
                                                 currentMonthEndDate);

            var dashboardData = MapDashboardData(personManagement.members,
                                                 personManagement.newComers,
                                                 finances.ToList());

            dashboardData.CurrentMonthAttendance = currentMonthAttendances;
            dashboardData.CurrentYearAttendance = currentYearAttendances;
            dashboardData.LastYearAttendance = lastYearAttendances;

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

        private GetDashboardDataResponseDto MapDashboardData(int members,
                                                                    int newComers,
                                                                    ICollection<Domain.Entities.FinanceAggregate.Finance> finances)
        {
            return new GetDashboardDataResponseDto
            {
                Members = members,
                Tithe = finances
                        .Where(f => f.FinanceTypeId == (int)FinanceEnum.Tithe)
                        .Sum(a => a.Amount),
                Thanksgiving = finances
                               .Where(t => t.FinanceTypeId == (int)FinanceEnum.Thanksgiving)
                               .Sum(a => a.Amount),
                Expenses = finances
                           .Where(t => t.FinanceTypeId == (int)FinanceEnum.Spending)
                           .Sum(a => a.Amount),
                Offering = finances
                           .Where(t => t.FinanceTypeId == (int)FinanceEnum.Offering ||
                                       t.FinanceTypeId == (int)FinanceEnum.MidWeekServiceOffering)
                           .Sum(a => a.Amount),
                NewComers = newComers
            };
        }
    }
}