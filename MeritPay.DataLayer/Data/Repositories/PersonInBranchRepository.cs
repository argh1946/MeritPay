using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;

namespace SampleProject.Infrastructure.Data
{
    public class PersonInBranchRepository : RepositoryBase<PersonInBranch>, IPersonInBranchRepository
    {
        public PersonInBranchRepository(MeritPayContext db) : base(db)
        {
        }

        public async Task<PersonInBranch> IsExsistAsync(int branchId, int personId)
        {
            var r = await _db.PersonInBranch.FirstOrDefaultAsync(x => x.BranchId == branchId && x.PersonId == personId);
            return r;
        }
    }
}