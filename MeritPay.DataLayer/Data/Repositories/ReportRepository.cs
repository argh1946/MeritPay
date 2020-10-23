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
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(MeritPayContext db) : base(db)
        {
        }

        public async Task<List<Report>> GetReportByPersonCodeAsync(int periodId, int personCode)
        {
            var r = await _db.Report.Where(d => d.PersonInBranch.Person.PersonCode == personCode && d.MeritPayFactor.PeriodId == periodId).ToListAsync();
            return r;
        }

        public async Task<bool> DeleteAllPersonInPeriodReportAsync(int periodId)
        {
            //return await Task.Run(() =>
            //{
            //    var list = _db.Report.Where(d => d.MeritPayFactor.PeriodId == periodId);
            //    _db.Report.RemoveRange(list);
            //    return true;
            //});

            var list = await _db.Report.Where(d => d.MeritPayFactor.PeriodId == periodId).ToListAsync();
            _db.Report.RemoveRange(list);
            return true;
        }
    }
}