namespace Money.Application.ServiceDTO
{
    public class CurrencyConversionResponseDTO
    {
        public int Id { get; set; }
        public required string FromCurrency { get; set; }
        public required string ToCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime ConversionDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
