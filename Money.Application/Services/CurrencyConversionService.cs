using AutoMapper;
using Microsoft.Extensions.Logging;
using Money.Application.Factories;
using Money.Application.ServiceDTO;
using Money.Domain.Repositories;

namespace Money.Application.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly ICurrencyConverterFactory _currencyConverterFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyConversionService> _logger;
        private readonly ICurrencyConversionRepository _currencyConversionRepository;


        public CurrencyConversionService(
            ICurrencyConverterFactory currencyConverterFactory,
            ICurrencyConversionRepository currencyConversionRepository,
            IMapper mapper,
            ILogger<CurrencyConversionService> logger)
        {
            _currencyConverterFactory = currencyConverterFactory;
            _currencyConversionRepository = currencyConversionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CurrencyConversionResponseDTO> ConvertCurrencyAsync(CurrencyConversionRequestDTO request)
        {
            try
            {
                _logger.LogInformation("Processing currency conversion request");

                var conversion = await _currencyConverterFactory.ConvertAsync(

                    request.FromCurrency,
                    request.ToCurrency,
                    request.Amount,
                    request.DepartmentId);

                return _mapper.Map<CurrencyConversionResponseDTO>(conversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing currency conversion request");
                throw;
            }
        }

        public async Task<CurrencyConversionResponseDTO> GetCurrencyConversionByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting conversion by ID: {Id}", id);

                var savedConversion = await _currencyConversionRepository.GetByIdAsync(id);

                return _mapper.Map<CurrencyConversionResponseDTO>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetCurrencyConversionsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                _logger.LogInformation("Getting conversion history by department ID: {DepartmentId}", departmentId);

                var savedConversion = await _currencyConversionRepository.GetByDepartmentIdAsync(departmentId);

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by department ID: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetCurrencyConversionsByCurrencyCodeAsync(string code)
        {
            try
            {
                _logger.LogInformation("Getting conversion history by currency code: {CurrencyCode}", code);

                var savedConversion = await _currencyConversionRepository.GetByCurrencyCodeAsync(code);

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by currency code: {CurrencyCode}", code);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetAllCurrencyConversionsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all conversion history");

                var savedConversion = await _currencyConversionRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all conversion history");
                throw;
            }
        }
    }
}
