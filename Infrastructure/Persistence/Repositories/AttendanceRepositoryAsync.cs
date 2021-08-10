using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.AttendanceAggregate;
using Domain.Entities.FinanceAggregate;
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
                                                                                       DateTime startDate,
                                                                                       DateTime endDate)
            => await _dbContext.Set<Attendance>()
                               .Where(f => f.TenantId == tenantId
                                           && f.ServiceDate >= startDate && f.ServiceDate <= endDate)
                               .ToListAsync();
    }
}