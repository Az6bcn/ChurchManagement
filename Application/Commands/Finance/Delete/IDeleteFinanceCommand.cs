using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Finance.Delete
{
    public interface IDeleteFinanceCommand
    {
        Task ExecuteAsync(int financeId, int tenantId);
    }
}
