using Money.Application.ServiceDTO;

namespace Money.Application.Services
{
    public interface ICurrencyConversionService
    {
        Task<CurrencyConversionResponseDTO> ConvertCurrencyAsync(CurrencyConversionRequestDTO request);
        Task<CurrencyConversionResponseDTO> GetCurrencyConversionByIdAsync(int id);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetCurrencyConversionsByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetCurrencyConversionsByCurrencyCodeAsync(string code);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetAllCurrencyConversionsAsync();
    }
}
