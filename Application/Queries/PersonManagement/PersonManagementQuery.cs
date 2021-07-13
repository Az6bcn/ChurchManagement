using Application.Interfaces.Repositories;

namespace Application.Queries.PersonManagement
{
    public class PersonManagementQuery: IQueryPersonManagement
    {
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;

    }
}