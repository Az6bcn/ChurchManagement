using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.AttendanceAggregate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AttendanceRepositoryAsync: GenericRepositoryAsync<Attendance>, IAttendanceRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Attendance?> GetAttendanceByIdAndTenantIdAsync(int attendanceId, int tenantId)
            => await _dbContext.Set<Attendance>()
                               .Include(a => a.Tenant)
                               .SingleOrDefaultAsync(a => a.AttendanceId == attendanceId && a.TenantId == tenantId);

        public async Task<IEnumerable<Attendance>> GetAttendancesBetweenDatesByTenantIdAsync(int tenantId,
                                                                                             DateOnly startDate,
                                                                                             DateOnly endDate)
            => await _dbContext.Set<Attendance>()
                               .Where(f => f.TenantId == tenantId
                                           && f.ServiceDate >= startDate.ToDateTime(new TimeOnly(0,0)) 
                                           && f.ServiceDate <= endDate.ToDateTime(new TimeOnly(0,0)))
                               .ToListAsync();

        public async Task<IEnumerable<Attendance>> GetAttendancesForLastYearAndCurrentYearByTenantIdAsync(int tenantId,
                                                                                                          DateOnly startDate,
                                                                                                          DateOnly endDate)
            => await _dbContext.Set<Attendance>()
                               .Where(f => f.TenantId == tenantId
                                           && f.ServiceDate >= startDate.ToDateTime(new TimeOnly(0,0)) 
                                           && f.ServiceDate <= endDate.ToDateTime(new TimeOnly(0,0)))
                               .ToListAsync();
    }
}