using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IMeritPayFactorRepository : IAsyncGenericRepository<MeritPayFactor>
    {
        Task<List<MeritPayFactor>> GetMeritPayFactorByPeriodIdAsync(int periodId);
    }
}
