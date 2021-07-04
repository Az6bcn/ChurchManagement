using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        ITenantRepositoryAsync Tenants {get;}
        IMemberRepositoryAsync Members {get;}
        IDepartmentReporsitory Departments {get;}

        Task<int> SaveChangesAsync();

    }
}
