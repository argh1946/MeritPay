using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace SampleProject.Infrastructure.Data
{
    public class PersonScoreRepository : RepositoryBase<PersonScore>, IPersonScoreRepository
    {
        public PersonScoreRepository(MeritPayContext db) : base(db)
        {
        }

        public async Task<bool> DeleteAllPersonScoreInPeriodAsync(int periodId)
        {
            var list = await _db.PersonScore.Where(d => d.ScoreSubIndex.ScoreIndex.MeritPayFactor.PeriodId == periodId).ToListAsync();
            _db.PersonScore.RemoveRange(list);
            return true;
        }

        public async Task<List<PersonScore>> GetPersonScoreByPersonCodeAsync(int periodId, int personCode)
        {
            var list = await _db.PersonScore.Where(d => d.ScoreSubIndex.ScoreIndex.MeritPayFactor.PeriodId == periodId &&
            d.PersonInBranch.Person.PersonCode == personCode).Include(i=>i.ScoreSubIndex.ScoreIndex).Include(i=>i.PersonInBranch.Person).OrderBy(d=>d.ScoreSubIndex).ToListAsync();

            return list;
        }
    }
}