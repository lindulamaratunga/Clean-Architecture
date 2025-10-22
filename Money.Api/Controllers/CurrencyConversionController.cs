using Microsoft.AspNetCore.Mvc;
using Money.Application.ServiceDTO;
using Money.Application.Services;

namespace Money.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyConversionController : ControllerBase
{
    private readonly ICurrencyConversionService _currencyConversionService;
    private readonly ILogger<CurrencyConversionController> _logger;

    public CurrencyConversionController(ICurrencyConversionService currencyService, ILogger<CurrencyConversionController> logger)
    {
        _currencyConversionService = currencyService;
        _logger = logger;
    }

    /// <summary>
    /// Convert currency from one to another and save as a conversion record
    /// </summary>
    /// <param name="request">Currency conversion request</param>
    /// <returns>Currency conversion result</returns>
    [HttpPost("convert")]
    [ProducesResponseType(typeof(CurrencyConversionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurrencyConversionResponseDTO>> ConvertCurrency([FromBody] CurrencyConversionRequestDTO request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _currencyConversionService.ConvertCurrencyAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument in currency conversion");
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Request",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting currency");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An error occurred while converting currency",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    /// <summary>
    /// Get specific currency conversion by ID
    /// </summary>
    /// <param name="id">Conversion ID</param>
    /// <returns>Currency conversion details</returns>
    [HttpGet("get-by-id/{id}")]
    [ProducesResponseType(typeof(CurrencyConversionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CurrencyConversionResponseDTO>> GetConversionById(int id)
    {
        try
        {
            var conversion = await _currencyConversionService.GetCurrencyConversionByIdAsync(id);

            if (conversion == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = $"Currency conversion with ID {id} not found",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(conversion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting conversion by ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An error occurred while retrieving the conversion",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    /// <summary>
    /// Get specific currency conversion by department ID
    /// </summary>
    /// <param name="departmentId">Department ID to filter conversions</param>
    /// <returns>List of currency conversions</returns>
    [HttpGet("get-by-department/{Id}")]
    [ProducesResponseType(typeof(CurrencyConversionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<CurrencyConversionResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<CurrencyConversionResponseDTO>> GetConversionByDepartment(int Id)
    {
        try
        {
            var history = await _currencyConversionService.GetCurrencyConversionsByDepartmentIdAsync(Id);
            return Ok(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting conversion history");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An error occurred while retrieving conversion history",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    /// <summary>
    /// Get specific currency conversion by Currency Code
    /// </summary>
    /// <param name="departmentId">Currency Code to filter conversions</param>
    /// <returns>List of currency conversions</returns>
    [HttpGet("get-by-currency/{code}")]
    [ProducesResponseType(typeof(CurrencyConversionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<CurrencyConversionResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<CurrencyConversionResponseDTO>> GetConversionByCurrencyCodeAsync(string code)
    {
        try
        {
            var history = await _currencyConversionService.GetCurrencyConversionsByCurrencyCodeAsync(code);
            return Ok(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting conversion history");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An error occurred while retrieving conversion history",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    /// <summary>
    /// Get all currency conversions
    /// </summary>
    /// <returns>Currency conversion details</returns>
    [HttpGet("get-All")]
    [ProducesResponseType(typeof(CurrencyConversionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CurrencyConversionResponseDTO>> GetAll()
    {
        try
        {
            var conversion = await _currencyConversionService.GetAllCurrencyConversionsAsync();

            if (conversion == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = $"Currency conversion not found",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(conversion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting conversion");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An error occurred while retrieving the conversion",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}
