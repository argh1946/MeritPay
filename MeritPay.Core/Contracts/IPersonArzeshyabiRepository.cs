using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IPersonArzeshyabiRepository : IAsyncGenericRepository<PersonArzeshyabi>
    {
        Task<PersonArzeshyabi> GetPersonArzeshyabiByPersonCodeAsync(int periodId, int personCode);
        Task<bool> DeleteAllPersonArzeshyabiInPeriodAsync(int periodId);
    }
}
