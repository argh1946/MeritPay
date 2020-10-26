using MeritPay.Core.Contracts;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases
{
    public class PersonArzeshyabiService : IPersonArzeshyabiService
    {
        private readonly IUnitOfWork _uow;
        public PersonArzeshyabiService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PersonArzeshyabi> GetPersonArzeshyabiByPersonCodeAsync(int periodId, int personCode)
        {
            var res = await _uow.PersonArzeshyabiRepository.GetPersonArzeshyabiByPersonCodeAsync(periodId, personCode);
            return res;
        }

        
      
    }
}
