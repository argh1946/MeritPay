using SampleProject.Core.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MeritPay.Core.Contracts;

namespace MeritPay.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected MeritPayContext _db { get; private set; }


        private IServiceProvider _serviceProvider;
        private bool _disposed;

        public UnitOfWork(MeritPayContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                        _db = null;
                    }
                }
                _disposed = true;
            }
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IPersonRepository PersonRepository => _serviceProvider.GetRequiredService<IPersonRepository>();
        public IPersonScoreRepository PersonScoreRepository => _serviceProvider.GetRequiredService<IPersonScoreRepository>();
        public IReportRepository ReportRepository => _serviceProvider.GetRequiredService<IReportRepository>();
        public IBranchRepository BranchRepository => _serviceProvider.GetRequiredService<IBranchRepository>();
        public IMeritPayFactorRepository MeritPayFactorRepository => _serviceProvider.GetRequiredService<IMeritPayFactorRepository>();
        public IPersonInBranchRepository PersonInBranchRepository => _serviceProvider.GetRequiredService<IPersonInBranchRepository>();
        public IPersonArzeshyabiRepository PersonArzeshyabiRepository => _serviceProvider.GetRequiredService<IPersonArzeshyabiRepository>();
    }
}