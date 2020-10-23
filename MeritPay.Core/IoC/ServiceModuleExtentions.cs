using MeritPay.Core.UseCases;
using MeritPay.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MeritPay.Core.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPersonService, PersonService>();
            serviceCollection.AddScoped<IReportService, ReportService>();
            serviceCollection.AddScoped<IImportDataService, ImportDataService>();           
            serviceCollection.AddScoped<IMeritPayFactorService, MeritPayFactorService>();           
        }
    }
}
