using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagements;
using Application.Queries.Tenants;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using Domain.ValueObjects;

namespace Application.Commands.PersonManagements.Update;

public class NewComerCommandUpdater : IUpdateNewComerCommand
{
    private readonly IQueryTenant _tenantQuery;
    private readonly IQueryPersonManagement _personManagementQuery;
    private readonly IPersonManagementRepositoryAsync _personManagementRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidatePersonManagementRequestDto _requestValidator;
    private readonly IMapper _mapper;

    public NewComerCommandUpdater(IQueryTenant tenantQuery,
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

    public async Task<UpdateNewComerResponseDto> ExecuteAsync(UpdateNewComerRequestDto request)
    {
        var newComer
            = await _personManagementQuery.GetNewComerByIdAsync(request.NewComerId,
                                                                request.TenantId);

        if (newComer is null)
            throw new ArgumentException($"{request.NewComerId} {request.NewComerId} not found");

        var person = Person.Create(request.TenantId,
                                   request.Name,
                                   request.Surname,
                                   request.DateAndMonthOfBirth,
                                   request.Gender,
                                   request.PhoneNumber);

        var personValidationErrors = person.Validate();

        if (personValidationErrors.Any())
            throw new ValidationException("Request failed validation",
                                          new Dictionary<string, object> { { "Request errors", string.Join(" , ", personValidationErrors) } });

        PersonManagement.AssignNewComer(newComer);
        PersonManagement.UpdateNewComer(person,
                                        request.DateAttended,
                                        request.ServiceTypeEnum,
                                        out var notification);

        if (notification.HasErrors)
            throw new ValidationException("Request failed validation", notification.Errors);
        
        _personManagementRepo.Update(PersonManagement.NewComer);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UpdateNewComerResponseDto>(PersonManagement.NewComer);
    }
}