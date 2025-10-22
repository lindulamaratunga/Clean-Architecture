using Money.Application.ExternalDTO;
using Refit;

namespace Money.Application.ExternalApi
{
    public interface IOpenExchangeRatesApi
    {
        [Get("/latest.json")]
        Task<OpenExchangeRatesApiResponseDTO> GetLatestRateAsync([AliasAs("app_id")] string appId, [AliasAs("base")] string baseCurrency, [AliasAs("symbols")] string symbols);
    }
}
