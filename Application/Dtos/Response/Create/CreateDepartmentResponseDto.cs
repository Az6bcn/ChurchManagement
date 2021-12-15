namespace Application.Dtos.Response.Create;

public class CreateDepartmentResponseDto
{
    public int DepartmentId { get; private set; }
    public int TenantId { get; private set; }
    public string Name { get; private set; }
}