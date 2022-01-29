using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagements;
using Application.Queries.Tenants;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using Domain.ValueObjects;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;


namespace Application.Commands.PersonManagements.Create;

public class MemberCommandCreator : ICreateMemberCommand
{
    private readonly IQueryTenant _tenantQuery;
    private readonly IQueryPersonManagement _personManagementQuery;
    private readonly IPersonManagementRepositoryAsync _personManagementRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidatePersonManagementRequestDto _requestValidator;
    private readonly IMapper _mapper;

    public MemberCommandCreator(IQueryTenant tenantQuery,
                                IQueryPersonManagement personManagementQuery,
                                IPersonManagementRepositoryAsync personManagementRepo,
                                IUnitOfWork unitOfWork,
                                IValidatePersonManagementRequestDto requestValidator,
                                IMapper mapper)
    {
        _tenantQuery = tenantQuery;
        _personManagementQuery = personManagementQuery;
        _personManagementRepo = personManagementRepo;
        _unitOfWork = unitOfWork;
        _requestValidator = requestValidator;
        _mapper = mapper;
    }

    public async Task<CreateMemberResponseDto> ExecuteAsync(CreateMemberRequestDto request)
    {
        var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);

        if (tenant is null)
            throw new ArgumentException("Invalid tenantId", nameof(request.TenantId));

        var person = Person.Create(request.TenantId,
                                   request.Name,
                                   request.Surname,
                                   request.DateAndMonthOfBirth,
                                   request.Gender,
                                   request.PhoneNumber);

        var personValidationErrors = person.Validate().ToList();
        if (personValidationErrors.Any())
            throw new ValidationException("Request failed validation",
                                                 new Dictionary<string, object>
                                                 {
                                                     { "Request errors", string.Join(" , ", personValidationErrors) }
                                                 });

        PersonManagementAggregate.CreateMember(person,
                                               tenant,
                                               request.IsWorker);
        var member = PersonManagementAggregate.Member;

        await _personManagementRepo.AddAsync<Member>(member);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CreateMemberResponseDto>(member);
    }
}