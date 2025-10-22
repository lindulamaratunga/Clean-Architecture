using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Money.Application.ExternalApi;
using Money.Application.ExternalDTO;

namespace Money.Application.ExternalServices
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IOpenExchangeRatesApi _api;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly string _apiKey;

        public ExchangeRateService(IOpenExchangeRatesApi api, ILogger<ExchangeRateService> logger, IConfiguration configuration)
        {
            _api = api;
            _logger = logger;
            _apiKey = configuration["OpenExchangeRates:ApiKey"] ?? throw new ArgumentNullException("OpenExchangeRates:ApiKey");
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            try
            {
                _logger.LogInformation("Getting exchange rate from {FromCurrency} to {ToCurrency}", fromCurrency, toCurrency);

                OpenExchangeRatesApiResponseDTO response = null;

                try
                {
                    response = await _api.GetLatestRateAsync(_apiKey, fromCurrency, toCurrency);
                }
                catch
                {
                    // External API error
                }

                if (response == null || response.Rates.First().Value == 0)
                {
                    return 0;
                }

                if (string.Equals(fromCurrency, toCurrency, StringComparison.OrdinalIgnoreCase))
                {
                    return -1;
                }

                _logger.LogInformation("Exchange rate calculated: {FromCurrency} to {ToCurrency} = {Rate}",
                        fromCurrency, toCurrency, response.Rates.First().Value);

                return response.Rates.First().Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting exchange rate from {FromCurrency} to {ToCurrency}", fromCurrency, toCurrency);
                throw;
            }
        }
    }
}
