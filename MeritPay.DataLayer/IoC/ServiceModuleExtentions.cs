using Microsoft.Extensions.DependencyInjection;
using MeritPay.Core.Contracts;
using MeritPay.Infrastructure.Data;
using System.Linq;
using SampleProject.Infrastructure.Data;

namespace MeritPay.Infrastructure.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
            serviceCollection.AddScoped<IBranchRepository, BranchRepository>();
            serviceCollection.AddScoped<IPersonInBranchRepository, PersonInBranchRepository>();
            serviceCollection.AddScoped<IMeritPayFactorRepository, MeritPayFactorRepository>();
            serviceCollection.AddScoped<IMeritPayFactorRepository, MeritPayFactorRepository>();
            serviceCollection.AddScoped<IReportRepository, ReportRepository>();
            
        }
    }
}