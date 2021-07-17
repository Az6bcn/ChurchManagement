using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.TenantAggregate;
using Domain.ProjectionEntities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantRepositoryAsync : GenericRepositoryAsync<Tenant>, ITenantRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public TenantRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Tenant>> GetTenantMembersByTenantGuidAsync(int tenantId)
            => await _dbContext
                     .Tenants
                     //.Include(t => t.Members.Where(m => m.TenantId == tenantId))
                     .Where(t => t.TenantId == tenantId)
                     .ToListAsync();

        public async Task<TenantDetailsProjection?> GetTenantByGuidIdAsync(Guid tenantGuidId)
        {
            var response = await _dbContext.Tenants
                                           .Where(t => t.TenantGuidId == tenantGuidId)
                                           .AsNoTracking()
                                           .Select(t => new TenantDetailsProjection
                                           {
                                               Name = t.Name,
                                               LogoUrl = t.LogoUrl,
                                               TenantId = t.TenantId
                                           })
                                           .SingleOrDefaultAsync();

            return response;
        }

        public async Task<Tenant?> GetMonthDashboardDataAsync(Guid tenantGuidId,
                                                              int tenantId)
        {
            var today = DateTime.Now;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = new DateTime(today.Year,
                                              today.Month,
                                              DateTime.DaysInMonth(today.Year, today.Month));

            var firstDayOfYear = new DateTime(today.Year, 01, 01);

            var response = await _dbContext.Tenants
                                           // .Include(t => t.Members)
                                           // .Include(t => t.Finances.Where(x => x.ServiceDate.Value.Date >= firstDayOfMonth.Date
                                           //                                     && x.ServiceDate.Value.Date <= lastDayOfMonth.Date))
                                           // .Include(t => t.Attendance.Where(x => x.ServiceDate.Date >= firstDayOfYear.Date
                                           //                                       && x.ServiceDate <= lastDayOfMonth.Date))
                                           // .Include(t => t.NewComers.Where(x => x.DateAttended.Date >= firstDayOfMonth.Date
                                           //                                      && x.DateAttended <= lastDayOfMonth.Date))
                                           // .ThenInclude(nc => nc.ServiceType)
                                           // .Include(t => t.Currency)
                                           // .Where(t => t.TenantId == tenantId && t.TenantGuidId == tenantGuidId)
                                           // .AsSplitQuery()
                                           .SingleOrDefaultAsync(t => t.TenantId == tenantId &&
                                                                      t.TenantGuidId == tenantGuidId);
            //&& t.TenantStausId == (int)TenantStatusEnum.Active);

            return response;
        }

        public async Task<IEnumerable<string>> GetTenantNamesAsync()
        {
            var response = await _dbContext.Tenants
                                           .AsNoTracking()
                                           .Select(x => x.Name)
                                           .ToListAsync();

            return response;
        }
    }
}