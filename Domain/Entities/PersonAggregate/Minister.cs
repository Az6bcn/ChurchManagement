using Domain.Entities.Helpers;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;
using Shared.Enums;

namespace Domain.Entities.PersonAggregate;

public class Minister : IEntity
{
    private Minister()
    {
    }

    internal Minister(Member member,
                      MinisterTitleEnum ministerTitleEnum)
    {
        var ministerTitleEnumValue = GetServiceTypeEnumValue(ministerTitleEnum);
        MinisterTitle = MinisterTitle.Create(ministerTitleEnumValue.Id, ministerTitleEnumValue.Value);

        MemberId = member.MemberId;
        MinisterTitleId = MinisterTitle.MinisterTitleId;
        TenantId = member.TenantId;
        Tenant = member.Tenant;
        CreatedAt = DateTime.UtcNow;
    }

    public int MinisterId { get; private set; }
    public int MemberId { get; private set; }
    public int MinisterTitleId { get; private set; }
    public int TenantId { get; private set; }
    public Tenant Tenant { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? Deleted { get; private set; }
    public MinisterTitle MinisterTitle { get; private set; }
        
    public Member Member { get; private set; }

    public string Title => MinisterTitle.Name;

    public static Minister Create(Member member,
                                  MinisterTitleEnum ministerTitle)
        => new(member, ministerTitle);

    public void Update(Minister minister, MinisterTitleEnum ministerTitleEnum)
    {
        MemberId = minister.MemberId;
        MinisterTitleId = (int)ministerTitleEnum;
        TenantId = minister.TenantId;
        Tenant = minister.Tenant;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        Deleted = DateTime.UtcNow;
    }
        
    private EnumValue GetServiceTypeEnumValue(MinisterTitleEnum ministerTitleEnum)
        => EnumService<MinisterTitleEnum>.GetValue(ministerTitleEnum);
}