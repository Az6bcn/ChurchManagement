using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Enums;
using Application.Helpers;
using Application.Interfaces.Repositories;

namespace Application.Queries.Tenant.TenantDashboardData
{
    public class TenantDashboardQuery: IQueryTenantDashboardData
    {
        private readonly ITenantRepository _tenantRepo;

        public TenantDashboardQuery(ITenantRepository tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }
        
        public async Task<QueryResult<DashboardDataDto>> ExecuteAsync(Guid tenantGuidId)
        {
            var tenant = await _tenantRepo.GetTenantByGuidIdAsync(tenantGuidId);
            var result = await _tenantRepo.GetMonthDashboardDataAsync(tenantGuidId, tenant.TenantId);

            if (result is null)
                return default;
            
            var today = DateTime.Now;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            var dashboardData = MapDashboardData(result, firstDayOfMonth, lastDayOfMonth);
            // var allAttendances = result.Attendance
            //     .GroupBy(x => x.ServiceDate.Date)
            //     .ToDictionary(x => x.Key,
            //         x => new AttendanceDto(x.ToList()));
            //
            // dashboardData.Attendance = allAttendances;
            
            var response = QueryResult<DashboardDataDto>.CreateQueryResult(dashboardData);

            return response;
        }

        private static DashboardDataDto MapDashboardData(Domain.Entities.TenantAggregate.Tenant? result, DateTime firstDayOfMonth, DateTime lastDayOfMonth)
        {
            return new DashboardDataDto
            {
                // Members = result.Members.Count(),
                // Tithe = result.Finances
                //     .Where(f => f.FinanceTypeId == (int) FinanceEnum.Tithe
                //                 && f.ServiceDate.Value.Date >= firstDayOfMonth.Date
                //                 && f.ServiceDate.Value.Date <= lastDayOfMonth.Date)
                //     .Sum(a => a.Amount),
                // Thanksgiving = result.Finances
                //     .Where(t => t.FinanceTypeId == (int) FinanceEnum.Thanksgiving
                //                 && t.ServiceDate.Value.Date >= firstDayOfMonth.Date
                //                 && t.ServiceDate.Value.Date <= lastDayOfMonth.Date)
                //     .Sum(a => a.Amount),
                // Expenses = result.Finances
                //     .Where(t => t.FinanceTypeId == (int) FinanceEnum.Spending
                //                 && t.ServiceDate.Value.Date >= firstDayOfMonth.Date
                //                 && t.ServiceDate.Value.Date <= lastDayOfMonth.Date)
                //     .Sum(a => a.Amount),
                // Offering = result.Finances
                //     .Where(t => t.FinanceTypeId == (int) FinanceEnum.Offering ||
                //                 t.FinanceTypeId == (int) FinanceEnum.MidWeekServiceOffering
                //                 && t.ServiceDate.Value.Date >= firstDayOfMonth.Date
                //                 && t.ServiceDate.Value.Date <= lastDayOfMonth.Date)
                //     .Sum(a => a.Amount),
                // NewComers = result.NewComers
                //     .Count(nc => nc.DateAttended.Date >= firstDayOfMonth.Date
                //                  && nc.DateAttended.Date <= lastDayOfMonth.Date),
                // MonthAttendance = result.Attendance.Where(a => a.ServiceDate.Date >= firstDayOfMonth.Date 
                //                                                && a.ServiceDate.Date <= lastDayOfMonth.Date)
                //     .Select(x =>
                //     {
                //         var sum = x.Male + x.Female + x.Children;
                //
                //         return sum;
                //     })
                //     .Sum()
            };
        }
    }
}