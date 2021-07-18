namespace Application.Dtos.Response.Get
{
    public class GetDepartmentsResponseDto
    {
        public int DepartmentId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
    }
}