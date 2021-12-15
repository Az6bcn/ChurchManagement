using Domain.Interfaces;

namespace Domain.Entities.TenantAggregate;

public class TenantStatus: IEntity
{
    public TenantStatus()
    {
            
    }
        
    internal TenantStatus(int id, string name)
    {
        TenantStatusId = id;
        Name = name;
    }
        
    public int TenantStatusId { get; private set; }
    public string Name { get;  private set; }
    public int TenantStatusValueObjectId => TenantStatusId;
        
        
    public static TenantStatus Create(int id, string name) => new (id, name);

}