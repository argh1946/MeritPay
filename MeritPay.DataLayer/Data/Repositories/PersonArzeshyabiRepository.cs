using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SampleProject.Infrastructure.Data
{
    public class PersonArzeshyabiRepository : RepositoryBase<PersonArzeshyabi>, IPersonArzeshyabiRepository
    {
        public PersonArzeshyabiRepository(MeritPayContext db) : base(db)
        {
        }       

        public async Task<PersonArzeshyabi> GetPersonArzeshyabiByPersonCodeAsync(int periodId, int personCode)
        {
            var r = await _db.PersonArzeshyabi.Where(d => d.PersonInBranch.Person.PersonCode == personCode && d.PeriodId == periodId).Include(i=>i.PersonInBranch.Person).Include(i=>i.PersonInBranch.Branch).FirstOrDefaultAsync();
            return r;
        }

        public async Task<bool> DeleteAllPersonArzeshyabiInPeriodAsync(int periodId)
        {
            var list = await _db.PersonArzeshyabi.Where(d => d.PeriodId == periodId).ToListAsync();
            _db.PersonArzeshyabi.RemoveRange(list);
            return true;
        }
    }
}