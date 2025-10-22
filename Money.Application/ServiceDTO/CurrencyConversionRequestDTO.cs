using System.ComponentModel.DataAnnotations;

namespace Money.Application.ServiceDTO
{
    public class CurrencyConversionRequestDTO
    {
        [Required]
        [MaxLength(3)]
        public string FromCurrency { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string ToCurrency { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 2147483647, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; } = 0.0m;

        [Required]
        [Range(1, 2147483647, ErrorMessage = "Amount must be greater than 0")]
        public int DepartmentId { get; set; } = 0;
    }
}
