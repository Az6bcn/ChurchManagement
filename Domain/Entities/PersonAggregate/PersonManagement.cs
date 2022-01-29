using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;
using Shared.Communications;
using Shared.Enums;

namespace Domain.Entities.PersonAggregate;

public class PersonManagement : IEntity, IAggregateRoot
{
    public static Member Member { get; private set; }
    public static NewComer? NewComer { get; private set; }
    public static Minister Minister { get; private set; }
    public static Department Department { get; private set; }
    public static DepartmentMembers? DepartmentMembers { get; private set; }

    public static Department AssignDepartment(Department department) => Department = department;

    public static void CreateDepartment(string name,
                                        Tenant tenant)
        => Department = Department.Create(name, tenant);

    public static void UpdateDepartment(string name) => Department.Update(name);

    public static void DeleteDepartment() => Department.Delete();


    public static Member AssignMember(Member member)
        => Member = member;

    public static void CreateMember(Person person,
                                    Tenant tenant,
                                    bool isWorker)
        => Member = Member.Create(person, tenant, isWorker);

    public static void UpdateMember(Person person,
                                    bool isWorker)
        => Member.UpdateMember(person, isWorker);

    public static void DeleteMember() => Member.Delete();


    public static void AssignNewComer(NewComer? newComer) => NewComer = newComer;

    public static void CreateNewComer(Person person,
                                      DateTime dateAttended,
                                      ServiceEnum serviceEnumType,
                                      Tenant tenant,
                                      out NotificationOutput notification)
    {
        notification = new NotificationOutput();
        NewComer = NewComer.Create(person, dateAttended, serviceEnumType, tenant, notification);
    }

    public static void UpdateNewComer(Person person,
                                      DateTime dateAttended,
                                      ServiceEnum serviceEnumType,
                                      out NotificationOutput notification)
    {
        notification = new NotificationOutput();
        NewComer?.Update(person, dateAttended, serviceEnumType, notification);
    }
    public static void DeleteNewComer() => NewComer?.Delete();

    public static void AssignMinister(Minister minister) => Minister = minister;

    public static void CreateMinister(Member member,
                                      MinisterTitleEnum ministerTitle)
        => Minister = Minister.Create(member, ministerTitle);

    public static void UpdateMinister(Minister minister,
                                      MinisterTitleEnum ministerTitle)
        => Minister.Update(minister, ministerTitle);

    public static void DeleteMinister() => Minister.Delete();

    public static void AssignDepartmentMembers(DepartmentMembers? departmentMember)
        => DepartmentMembers = departmentMember;

    public static void AssignMemberToDepartment(Member member,
                                                Department department,
                                                bool isHod,
                                                DateTime dateJoined,
                                                out NotificationOutput notification)
    {
        notification = new NotificationOutput();
        
        DepartmentMembers = DepartmentMembers.Assign(department, member, isHod, dateJoined, notification);
    }

    public static void UnAssignMemberFromDepartment()
        => DepartmentMembers?.UnAssignMember();
    public static void AssignAsHod() => DepartmentMembers?.AssignAsHod();

    public static void UnAssignAsHod() => DepartmentMembers?.RemoveAsHod();

        
}