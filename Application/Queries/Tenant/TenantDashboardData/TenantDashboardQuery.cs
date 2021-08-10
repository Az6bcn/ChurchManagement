using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
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
            if (!startDate.HasValue || !endDate.HasValue)
            {
                var today = DateTime.UtcNow;
                startDate = new DateTime(today.Year, today.Month, 1);
                endDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            }

            var personManagement = await _personMangeRpo.GetPersonsBetweenDatesByTenantIdAsync(tenantId);
            var finances = await _financeRepo.GetFinancesBetweenDatesByTenantIdAsync(tenantId,
                            startDate.Value.Date,
                            endDate.Value.Date);
            var attendances
                = await _attendanceRepo.GetAttendancesBetweenDatesByTenantIdAsync(tenantId,
                   startDate.Value.Date,
                   endDate.Value.Date);

            var dashboardData = MapDashboardData(personManagement.members, personManagement.newComers, finances.ToList(), 
            attendances.ToList());
            
            var allAttendances = attendances
                .GroupBy(x => x.ServiceDate.Date)
                .ToDictionary(x => x.Key,
                    x => new GetAttendanceResponseDto(x.ToList()));
            
            dashboardData.Attendance = allAttendances;

            var response = QueryResult<GetDashboardDataResponseDto>.CreateQueryResult(dashboardData);

            return response;
        }

        private static GetDashboardDataResponseDto MapDashboardData(int members,
                                                                    int newComers,
                                                                    ICollection<Domain.Entities.FinanceAggregate.Finance> finances,
                                                                    ICollection<Domain.Entities.AttendanceAggregate.Attendance> attendances)
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
                NewComers = newComers,
                MonthAttendance = attendances
                                  .Select(x =>
                                  {
                                      var sum = x.Male + x.Female + x.Children;

                                      return sum;
                                  })
                                  .Sum()
            };
        }
    }
}