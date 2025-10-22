using Microsoft.EntityFrameworkCore;
using Money.Domain.Models;
using Money.Domain.Repositories;
using Money.Infrastructure.Data;

namespace Money.Infrastructure.Repositories
{
    public class CurrencyConversionRepository : ICurrencyConversionRepository
    {
        private readonly MoneyDbContext _context;

        public CurrencyConversionRepository(MoneyDbContext context)
        {
            _context = context;
        }

        public async Task<CurrencyConversion> AddAsync(CurrencyConversion conversion)
        {
            _context.CurrencyConversions.Add(conversion);
            await _context.SaveChangesAsync();
            return conversion;
        }

        public async Task<CurrencyConversion> GetByIdAsync(int id)
        {
            return await _context.CurrencyConversions.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<CurrencyConversion>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.CurrencyConversions
                .Where(c => c.DepartmentId == departmentId)
                .OrderByDescending(c => c.ConversionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CurrencyConversion>> GetByCurrencyCodeAsync(string code)
        {
            return await _context.CurrencyConversions
                .Where(c => c.ToCurrency == code)
                .OrderByDescending(c => c.ConversionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CurrencyConversion>> GetAllAsync()
        {
            return await _context.CurrencyConversions
                .OrderByDescending(c => c.ConversionDate)
                .ToListAsync();
        }
    }
}
