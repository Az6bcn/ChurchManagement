using Application.Dtos.Request.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Attendances;
using Application.Queries.Tenants;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Attendances.Update;

public class AttendanceUpdaterCommand: IUpdateAttendanceCommand
{
    private readonly IQueryAttendance _attendanceQuery;
    private readonly IAttendanceRepositoryAsync _attendanceRepo;
    private readonly IQueryTenant _tenantQuery;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidateAttendanceInDomain _validator;
    private readonly IMapper _mapper;

    public AttendanceUpdaterCommand(IQueryAttendance attendanceQuery,
                                    IAttendanceRepositoryAsync attendanceRepo,
                                    IQueryTenant tenantQuery,
                                    IUnitOfWork unitOfWork,
                                    IValidateAttendanceInDomain validator,
                                    IMapper mapper)
    {
        _attendanceQuery = attendanceQuery;
        _attendanceRepo = attendanceRepo;
        _tenantQuery = tenantQuery;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(UpdateAttendanceRequestDto request)
    {
        var finance = await _attendanceQuery.GetAttendanceByIdAndTenantIdAsync(request.AttendanceId, request.TenantId);
        if (finance is null)
            throw new InvalidOperationException($"Attendance {request.AttendanceId} not found");
            
        finance.Update(_validator,
                       finance.Tenant,
                       request.ServiceDate,
                       request.Male,
                       request.Female,
                       request.Children,
                       request.NewComers,
                       request.ServiceTypeEnum);

        _attendanceRepo.Update(finance);
        await _unitOfWork.SaveChangesAsync();
    }
}