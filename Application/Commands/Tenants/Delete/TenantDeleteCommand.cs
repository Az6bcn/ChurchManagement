using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Tenants;

namespace Application.Commands.Tenants.Delete;

public class TenantDeleteCommand: IDeleteTenantCommand
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantRepositoryAsync _tenantRepo;
    private readonly IQueryTenant _tenantQuery;

    public TenantDeleteCommand(IUnitOfWork unitOfWork,
                               ITenantRepositoryAsync tenantRepo,
                               IQueryTenant tenantQuery)
    {
        _unitOfWork = unitOfWork;
        _tenantRepo = tenantRepo;
        _tenantQuery = tenantQuery;
    }

    public async Task ExecuteAsync(int tenantId)
    {
        if (tenantId <= 0)
            throw new ArgumentOutOfRangeException(nameof(tenantId), "Invalid tenantId");
            
        var tenant = await _tenantQuery.GetTenantByIdAsync(tenantId);

        if (tenant is null)
            throw new ArgumentException($"Tenant {tenantId} not found");

        tenant.Delete();

        _tenantRepo.Update(tenant);
        await _unitOfWork.SaveChangesAsync();
    }
}