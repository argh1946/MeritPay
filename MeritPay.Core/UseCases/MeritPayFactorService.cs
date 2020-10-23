using MeritPay.Core.Contracts;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases
{
    public class MeritPayFactorService : IMeritPayFactorService
    {
        private readonly IUnitOfWork _uow;
        public MeritPayFactorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<MeritPayFactor> GetMeritPayByIdAsync(int id)
        {
            return await _uow.MeritPayFactorRepository.GetByIdAsync(id);
        }
    }
}
