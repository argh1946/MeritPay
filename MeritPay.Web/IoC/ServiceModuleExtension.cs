using AutoMapper;
using MeritPay.WebApi.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace MeritPay.WebApi.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterApiServices(this IServiceCollection serviceCollection)
        {
            var automapper = RegisterAutoMapperService();
            serviceCollection.AddSingleton(automapper);
        }

        private static IMapper RegisterAutoMapperService()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return mapper;
        }
    }
}
