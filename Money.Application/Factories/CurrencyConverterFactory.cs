using AutoMapper;
using Microsoft.Extensions.Logging;
using Money.Application.ExternalServices;
using Money.Domain.Models;
using Money.Domain.Repositories;

namespace Money.Application.Factories
{
    public class CurrencyConverterFactory : ICurrencyConverterFactory
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICurrencyConversionRepository _conversionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyConverterFactory> _logger;

        public CurrencyConverterFactory(
            IExchangeRateService exchangeRateService,
            ICurrencyConversionRepository conversionRepository,
            IMapper mapper,
            ILogger<CurrencyConverterFactory> logger)
        {
            _exchangeRateService = exchangeRateService;
            _conversionRepository = conversionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CurrencyConversion> ConvertAsync(string fromCurrency, string toCurrency, decimal amount, int departmentId)
        {
            try
            {
                _logger.LogInformation("Starting currency conversion: {Amount} {FromCurrency} to {ToCurrency}",
                    amount, fromCurrency, toCurrency);

                if (fromCurrency == null)
                {
                    throw new ArgumentException($"Currency {fromCurrency} is not supported");
                }

                if (toCurrency == null)
                {
                    throw new ArgumentException($"Currency {toCurrency} is not supported");
                }

                // Get exchange rate
                var exchangeRate = await _exchangeRateService.GetExchangeRateAsync(fromCurrency, toCurrency);

                if (exchangeRate == 0)
                {
                    throw new ArgumentException($"No exchange rates received from API {fromCurrency} to {toCurrency}");
                }

                if (exchangeRate == -1)
                {
                    throw new ArgumentException("Cannot perform conversion for the same currency");
                }

                // Calculate converted amount
                var convertedAmount = Math.Round(amount * exchangeRate, 2);

                // Create conversion record
                var conversion = new CurrencyConversion
                {
                    FromCurrency = fromCurrency.ToUpper(),
                    ToCurrency = toCurrency.ToUpper(),
                    Amount = amount,
                    ConvertedAmount = convertedAmount,
                    ExchangeRate = exchangeRate,
                    ConversionDate = DateTime.UtcNow,
                    DepartmentId = departmentId
                };

                // Save conversion to database
                var savedConversion = await _conversionRepository.AddAsync(conversion);

                _logger.LogInformation("Currency conversion completed successfully. Id: {Id}",
                    savedConversion.Id);

                return savedConversion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during currency conversion: {Amount} {FromCurrency} to {ToCurrency}",
                    amount, fromCurrency, toCurrency);
                throw;
            }
        }
    }
}
