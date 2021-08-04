using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.UnitOfWork;
using AutoMapper;

namespace Application.Commands.PersonManagement.Create
{
    public interface ICreateFinanceCommand
    {
        Task ExecuteAsync(CreateFinanceRequestDto request);

    }
}