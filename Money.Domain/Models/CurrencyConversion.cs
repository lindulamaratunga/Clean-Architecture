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
}
