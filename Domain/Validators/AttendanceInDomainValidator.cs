using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validators
{
    public class AttendanceInDomainValidator: IValidateAttendanceInDomain
    {
        public bool Validate(DateTime serviceDate,
                             int male, 
                             int female,
                             int children,
                             int newComer,
                             out Dictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            
            if(serviceDate == new DateTime())
                errors.Add(nameof(serviceDate), "Date is required");
            
            if(male < 0 || female < 0 || children< 0 || newComer < 0)
                errors.Add(nameof(serviceDate), "Count cannot be negative number");

            return !errors.Any();
        }
    }
}