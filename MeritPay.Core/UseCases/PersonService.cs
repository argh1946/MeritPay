using MeritPay.Core.Contracts;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;
        public PersonService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Person> GetPersonByUesrIdAsync(int userId)
        {  
            var res = await _uow.PersonRepository.GetByIdAsync(userId);
            return res;
        }

    }
}
