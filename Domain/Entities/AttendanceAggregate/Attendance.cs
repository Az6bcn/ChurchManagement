using Domain.Entities.Helpers;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.Validators;
using Domain.ValueObjects;
using Shared.Communications;
using Shared.Enums;

namespace Domain.Entities.AttendanceAggregate;

public class Attendance : IEntity, IAggregateRoot
{
    public Attendance()
    {
    }

    internal Attendance(Tenant tenant,
                        DateOnly serviceDate,
                        int male,
                        int female,
                        int children,
                        int newComers,
                        ServiceEnum serviceTypeEnum)
    {

        var serviceEnumValue = GetServiceTypeEnumValue(serviceTypeEnum);
        Tenant = tenant;
        TenantId = tenant.TenantId;
        ServiceTypeId = serviceEnumValue.Id;
        Male = male;
        Female = female;
        Children = children;
        NewComers = newComers;
        ServiceDate = serviceDate.ToDateTime(new TimeOnly(0,0));
        CreatedAt = DateTime.UtcNow;
    }

    public int AttendanceId { get; private set; }
    public int TenantId { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public int Male { get; private set; }
    public int Female { get; private set; }
    public int Children { get; private set; }
    public int NewComers { get; private set; }
    public int ServiceTypeId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? Deleted { get; private set; }
    public Tenant Tenant { get; private set; }
    public ServiceType ServiceType { get; private set; }

    public static Attendance? Create(IValidateAttendanceInDomain validator,
                                     Tenant tenant,
                                     DateOnly serviceDate,
                                     int male,
                                     int female,
                                     int children,
                                     int newComers,
                                     ServiceEnum serviceTypeEnum,
                                     out NotificationOutput notification)
    {
        notification = new NotificationOutput();

        if (!validator.Validate(serviceDate,
                                male,
                                female,
                                children,
                                newComers,
                                out var errors))
        {
            notification.AddErrors(errors);
            return null;
        }

        return new(tenant,
                   serviceDate,
                   male,
                   female,
                   children,
                   newComers,
                   serviceTypeEnum);
    }

    public void Update(IValidateAttendanceInDomain validator,
                       Tenant tenant,
                       DateOnly serviceDate,
                       int male,
                       int female,
                       int children,
                       int newComers,
                       ServiceEnum serviceTypeEnum,
                       out NotificationOutput notification)
    {
        notification = new NotificationOutput();

        if (!validator.Validate(serviceDate,
                                male,
                                female,
                                children,
                                newComers,
                                out var errors))
        {
            notification.AddErrors(errors);
            return;
        }

        var serviceEnumValue = GetServiceTypeEnumValue(serviceTypeEnum);

        Tenant = tenant;
        TenantId = tenant.TenantId;
        ServiceTypeId = serviceEnumValue.Id;
        Male = male;
        Female = female;
        Children = children;
        NewComers = newComers;
        ServiceDate = serviceDate.ToDateTime(new TimeOnly(0,0));
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete() => Deleted = DateTime.UtcNow;

    private EnumValue GetServiceTypeEnumValue(ServiceEnum serviceEnum)
        => EnumService<ServiceEnum>.GetValue(serviceEnum);
}