using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;

namespace SampleProject.Infrastructure.Data
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(MeritPayContext db) : base(db)
        {
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var data = await _db.Person.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
    }
}