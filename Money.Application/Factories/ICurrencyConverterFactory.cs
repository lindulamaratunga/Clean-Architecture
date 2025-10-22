using Money.Domain.Models;

namespace Money.Application.Factories
{
    public interface ICurrencyConverterFactory
    {
        Task<CurrencyConversion> ConvertAsync(string fromCurrency, string toCurrency, decimal amount, int departmentId);
    }
}
