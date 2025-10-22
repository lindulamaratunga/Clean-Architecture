using Money.Application.ExternalServices;
using Money.Application.Factories;
using Money.Application.Services;
using Money.Domain.Repositories;
using Money.Infrastructure.Repositories;

namespace Money.Api
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add Repositories
            services.AddScoped<ICurrencyConversionRepository, CurrencyConversionRepository>();

            // Add Services
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();

            // Add Factory
            services.AddScoped<ICurrencyConverterFactory, CurrencyConverterFactory>();

            return services;
        }
    }
}