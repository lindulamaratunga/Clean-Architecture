using Money.Domain.Models;

namespace Money.Domain.Repositories
{
    public interface ICurrencyConversionRepository
    {
        Task<CurrencyConversion> AddAsync(CurrencyConversion conversion);
        Task<CurrencyConversion> GetByIdAsync(int id);
        Task<IEnumerable<CurrencyConversion>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<CurrencyConversion>> GetByCurrencyCodeAsync(string code);
        Task<IEnumerable<CurrencyConversion>> GetAllAsync();
    }
}
