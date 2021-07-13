using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.RequestValidators;

namespace Application.Commands.PersonManagement.Create
{
    public class DepartmentCommandCreator: ICreateDepartmentCommand
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;

        public DepartmentCommandCreator(IQueryPersonManagement personManagementQuery,
                                        IUnitOfWork unitOfWork,
                                        IValidatePersonManagementRequestDto requestValidator)
        {
            _personManagementQuery = personManagementQuery;
            _unitOfWork = unitOfWork;
            _requestValidator = requestValidator;
        }

        public async Task<CreateDepartmentResponseDto> ExecuteAsync(CreateDepartmentRequestDto request)
        {
            return null;
        }
    }
}