using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IPersonScoreRepository : IAsyncGenericRepository<PersonScore>
    {
        Task<List<PersonScore>> GetPersonScoreByPersonCodeAsync(int periodId, int personCode);

        Task<bool> DeleteAllPersonScoreInPeriodAsync(int periodId);
    }
}
