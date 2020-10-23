using System;
using System.Threading.Tasks;

namespace MeritPay.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        IReportRepository ReportRepository { get; }
        IPersonRepository PersonRepository { get; }
        IBranchRepository BranchRepository { get; }
        IMeritPayFactorRepository MeritPayFactorRepository { get; }
        IPersonInBranchRepository PersonInBranchRepository { get; }
    }
}
