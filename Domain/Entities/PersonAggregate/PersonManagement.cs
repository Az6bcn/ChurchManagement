using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.PersonAggregate
{
    public class PersonManagement : IEntity, IAggregateRoot
    {
        public static Member Member { get; private set; }
        public static NewComer NewComer { get; private set; }
        public static Minister Minister { get; private set; }
        public static Department Department { get; private set; }

        public static void CreateDepartment(string name,
                                            Tenant tenant)
        => Department = Department.Create(name, tenant);

        public static Department AssignDepartment(Department department) 
            => Department = department;

        public static void UpdateDepartment(string name)
            => Department.Update(name);

        public static void DeleteDepartment()
            => Department.Delete();
        
        public static Member AssignMember(Member member ) => Member = member;

        public static void CreateMember(Person person,
                                        Tenant tenant,
                                        bool isWorker)
            => Member = Member.Create(person, tenant, isWorker);
    }
}