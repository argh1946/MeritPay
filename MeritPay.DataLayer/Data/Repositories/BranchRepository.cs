using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeritPay.Infrastructure.Data.Repositories;
using MeritPay.Core.Entities;
using MeritPay.Infrastructure.Data;
using MeritPay.Core.Contracts;

namespace SampleProject.Infrastructure.Data
{
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        public BranchRepository(MeritPayContext db) : base(db)
        {
        }

       
    }
}