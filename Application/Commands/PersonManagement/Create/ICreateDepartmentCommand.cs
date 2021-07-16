using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Update;

namespace Application.Commands.PersonManagement.Create
{
    public interface ICreateDepartmentCommand
    {
        Task<CreateDepartmentResponseDto> ExecuteAsync(CreateDepartmentRequestDto request);
    }
}