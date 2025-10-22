using System.ComponentModel.DataAnnotations;

namespace Money.Domain.Models
{
    public class CurrencyConversion
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public required string ToCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime ConversionDate { get; set; }
        public int DepartmentId { get; set; }
    }

    //public class CurrencyConversion
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [Required]
    //    [MaxLength(3)]
    //    public string FromCurrency { get; set; } = string.Empty;

    //    [Required]
    //    [MaxLength(3)]
    //    public required string ToCurrency { get; set; }

    //    public decimal ExchangeRate { get; set; }

    //    [Required]
    //    [Range(0.00, 2147483647, ErrorMessage = "Amount must be greater than 0")]
    //    public decimal Amount { get; set; }

    //    [Required]
    //    public decimal ConvertedAmount { get; set; }
    //    [Range(0.00, 2147483647, ErrorMessage = "Amount must be greater than 0")]

    //    [Required]
    //    public DateTime ConversionDate { get; set; } = DateTime.UtcNow;

    //    [Required]
    //    [Range(1, 2147483647, ErrorMessage = "Amount must be greater than 0")]
    //    public int DepartmentId { get; set; }

    //}
}
