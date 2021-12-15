using Application.Dtos.Request.Create;

namespace Application.Dtos.Request.Update;

public class UpdateDepartmentRequestDto: CreateDepartmentRequestDto
{
    public int DepartmentId { get; set; }   
}