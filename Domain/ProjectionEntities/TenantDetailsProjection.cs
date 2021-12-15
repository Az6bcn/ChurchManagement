namespace Domain.ProjectionEntities;

public class TenantDetailsProjection
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public int TenantId { get; set; }
    public string CurrencyCode { get; set; }
}