using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Application.ExternalServices;
using Money.Application.Factories;
using Money.Domain.Models;
using Money.Domain.Repositories;
using Moq;

namespace Money.Tests.Application
{
    [TestClass]
    public class CurrencyConverterFactoryTests
    {
        private Mock<IExchangeRateService> _mockExchangeRateService;
        private Mock<ICurrencyConversionRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<CurrencyConverterFactory>> _mockLogger;
        private CurrencyConverterFactory _factory;
        private IFixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockExchangeRateService = new Mock<IExchangeRateService>();
            _mockRepository = new Mock<ICurrencyConversionRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CurrencyConverterFactory>>();

            _factory = new CurrencyConverterFactory(
                _mockExchangeRateService.Object,
                _mockRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object);
        }

        [TestMethod]
        public async Task ConvertAsync_Convert()
        {
            // Arrange
            var fromCurrency = "USD";
            var toCurrency = "EUR";
            var amount = 10m;
            var departmentId = 1;
            var exchangeRate = 0.857829m;
            var expectedConvertedAmount = 8.58m;

            var expectedConversion = _fixture.Build<CurrencyConversion>()
                .With(x => x.FromCurrency, fromCurrency.ToUpper())
                .With(x => x.ToCurrency, toCurrency.ToUpper())
                .With(x => x.Amount, amount)
                .With(x => x.ConvertedAmount, expectedConvertedAmount)
                .With(x => x.ExchangeRate, exchangeRate)
                .With(x => x.DepartmentId, departmentId)
                .With(x => x.ConversionDate, DateTime.UtcNow)
                .Create();

            _mockExchangeRateService
                .Setup(x => x.GetExchangeRateAsync(fromCurrency, toCurrency))
                .ReturnsAsync(exchangeRate);

            _mockRepository
                .Setup(x => x.AddAsync(It.IsAny<CurrencyConversion>()))
                .ReturnsAsync((CurrencyConversion c) => c);

            // Act
            var result = await _factory.ConvertAsync(fromCurrency, toCurrency, amount, departmentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fromCurrency.ToUpper(), result.FromCurrency);
            Assert.AreEqual(toCurrency.ToUpper(), result.ToCurrency);
            Assert.AreEqual(amount, result.Amount);
            Assert.AreEqual(expectedConvertedAmount, result.ConvertedAmount);
            Assert.AreEqual(exchangeRate, result.ExchangeRate);
            Assert.AreEqual(departmentId, result.DepartmentId);

            _mockExchangeRateService.Verify(x => x.GetExchangeRateAsync(fromCurrency, toCurrency), Times.Once);
            _mockRepository.Verify(x => x.AddAsync(It.IsAny<CurrencyConversion>()), Times.Once);
        }
    }
}
