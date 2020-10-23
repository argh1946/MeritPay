using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IPersonInBranchRepository : IAsyncGenericRepository<PersonInBranch>
    {
       Task<PersonInBranch> IsExsistAsync(int branchId, int personId);

    }
}
