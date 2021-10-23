using System;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Attendance;
using Application.Queries.Tenant;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Attendance.Create
{
    public class AttendanceCreatorCommand : ICreateAttendanceCommand
    {
        private readonly IQueryAttendance _attendanceQuery;
        private readonly IAttendanceRepositoryAsync _attendanceRepo;
        private readonly IQueryTenant _tenantQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateAttendanceInDomain _validator;
        private readonly IMapper _mapper;

        public AttendanceCreatorCommand(IQueryAttendance attendanceQuery,
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

        public async Task ExecuteAsync(CreateAttendanceRequestDto request)
        {
            var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);
            if (tenant is null)
                throw new ArgumentException("Invalid tenantId", nameof(request.TenantId));

            var attendance = Domain.Entities.AttendanceAggregate.Attendance.Create(_validator,
                                                                          tenant,
                                                                          request.ServiceDate,
                                                                          request.Male,
                                                                          request.Female,
                                                                          request.Children,
                                                                          request.NewComers,
                                                                          request.ServiceTypeEnum);

            await _attendanceRepo.AddAsync(attendance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}