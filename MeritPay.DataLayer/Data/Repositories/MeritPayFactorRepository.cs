using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using System.Linq;

namespace SampleProject.Infrastructure.Data
{
    public class MeritPayFactorRepository : RepositoryBase<MeritPayFactor>, IMeritPayFactorRepository
    {
        public MeritPayFactorRepository(MeritPayContext db) : base(db)
        {
        }

        public async Task<List<MeritPayFactor>> GetMeritPayFactorByPeriodIdAsync(int periodId)
        {
            var items = await _db.MeritPayFactor.Where(d => d.PeriodId == periodId).ToListAsync();
            return items;
        }
    }
}