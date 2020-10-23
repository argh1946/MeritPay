using MeritPay.Core.Contracts;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _uow;
        public ReportService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Report>> GetReportByPersonCodeAsync(int periodId, int personCode)
        {
            var res = await _uow.ReportRepository.GetReportByPersonCodeAsync(periodId,personCode);
            return res;
        }

      
    }
}
