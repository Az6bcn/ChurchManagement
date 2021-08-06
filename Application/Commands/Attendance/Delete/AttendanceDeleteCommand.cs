using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Attendance;
using Application.Queries.Finance;
using Application.Queries.Tenant;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Attendance.Delete
{
    public class AttendanceDeleteCommand: IDeleteAttendanceCommand
    {
        private readonly IQueryAttendance _attendanceQuery;
        private readonly IAttendanceRepositoryAsync _attendanceRepo;
        private readonly IQueryTenant _tenantQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateAttendanceInDomain _validator;
        private readonly IMapper _mapper;

        public AttendanceDeleteCommand(IQueryAttendance attendanceQuery,
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

        public async Task ExecuteAsync(int attendanceId, int tenantId)
        {
            var finance = await _attendanceQuery.GetAttendanceByIdAndTenantIdAsync(attendanceId, tenantId);
            if (finance is null)
                throw new InvalidOperationException($"Attendance {attendanceId} not found");

            finance.Delete();

            _attendanceRepo.Update(finance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
