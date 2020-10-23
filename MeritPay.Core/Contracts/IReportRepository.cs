using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IReportRepository : IAsyncGenericRepository<Report>
    {
        Task<List<Report>> GetReportByPersonCodeAsync(int periodId, int personCode);
        Task<bool> DeleteAllPersonInPeriodReportAsync(int periodId);
    }
}
