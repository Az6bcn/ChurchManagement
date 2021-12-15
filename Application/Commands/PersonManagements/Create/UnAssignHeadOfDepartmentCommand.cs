using Application.Dtos.Request.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagements;
using Application.Queries.Tenants;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagements.Create;

public class UnAssignHeadOfDepartmentCommand: IUnAssignHeadOfDepartmentCommand
{
    private readonly IQueryTenant _tenantQuery;
    private readonly IQueryPersonManagement _personManagementQuery;
    private readonly IPersonManagementRepositoryAsync _personManagementRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UnAssignHeadOfDepartmentCommand(IQueryTenant tenantQuery,
                                           IQueryPersonManagement personManagementQuery,
                                           IPersonManagementRepositoryAsync personManagementRepo,
                                           IUnitOfWork unitOfWork,
                                           IMapper mapper)
    {
        _tenantQuery = tenantQuery;
        _personManagementQuery = personManagementQuery;
        _personManagementRepo = personManagementRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(AssignMemberToDepartmentRequestDto request)
    {
        var department =
            await _personManagementQuery.GetDepartmentIdAsync(request.DepartmentId, request.TenantId);
        var member =
            await _personManagementQuery.GetMemberByIdAsync(request.MemberId, request.TenantId);

        if (department is null || member is null)
            throw
                new ArgumentException($"Member {request.MemberId} or Department {request.DepartmentId} not found");

        var departmentMember =
            await _personManagementQuery.GetDepartmentMemberAsync(request.DepartmentId,
                                                                  request.MemberId,
                                                                  request.TenantId);

        PersonManagementAggregate.AssignDepartmentMembers(departmentMember);

        PersonManagementAggregate.UnAssignAsHod();

        _personManagementRepo.Update<DepartmentMembers>(PersonManagementAggregate.DepartmentMembers);
        await _unitOfWork.SaveChangesAsync();
    }
}