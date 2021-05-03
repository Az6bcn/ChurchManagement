using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        ITenantRepository Tenants {get;}
        IMemberRepository Members {get;}
        IDepartmentReporsitory Departments {get;}

        Task<int> SaveChangesAsync();

    }
}
