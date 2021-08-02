using Application.Dtos.Request.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Create
{
    public interface IAssignHeadOfDepartmentCommand
    {
        Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
    }
}
