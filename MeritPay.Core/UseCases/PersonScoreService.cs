using MeritPay.Core.Contracts;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases
{
    public class PersonScoreService : IPersonScoreService
    {
        private readonly IUnitOfWork _uow;
        public PersonScoreService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<PersonScore>> GetPersonScoreByPersonCodeAsync(int periodId, int personCode)
        {
            var res = await _uow.PersonScoreRepository.GetPersonScoreByPersonCodeAsync(periodId, personCode);
            return res;
        }
    }
}
