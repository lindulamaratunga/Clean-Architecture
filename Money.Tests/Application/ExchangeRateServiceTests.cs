using AutoFixture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Application.ExternalApi;
using Money.Application.ExternalDTO;
using Money.Application.ExternalServices;
using Moq;

namespace Money.Tests.Application
{
    [TestClass]
    public class ExchangeRateServiceTests
    {
        private Mock<IOpenExchangeRatesApi> _mockApi;
        private Mock<ILogger<ExchangeRateService>> _mockLogger;
        private Mock<IConfiguration> _mockConfiguration;
        private ExchangeRateService _service;
        //private IFixture _fixture;
        private const string TestApiKey = "test-api-key";

        [TestInitialize]
        public void Setup()
        {
            //_fixture = new Fixture();
            _mockApi = new Mock<IOpenExchangeRatesApi>();
            _mockLogger = new Mock<ILogger<ExchangeRateService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Setup configuration to return test API key
            _mockConfiguration
                .Setup(x => x["OpenExchangeRates:ApiKey"])
                .Returns(TestApiKey);

            _service = new ExchangeRateService(
                _mockApi.Object,
                _mockLogger.Object,
                _mockConfiguration.Object);
        }

        [TestMethod]
        public async Task GetExchangeRateAsync_ValidCurrencies_ReturnsExchangeRate()
        {
            // Arrange
            var fromCurrency = "usd";
            var toCurrency = "eur";
            var expectedRate = 0.857829m;

            var apiResponse = new OpenExchangeRatesApiResponseDTO
            {
                Rates = new Dictionary<string, decimal>
                {
                    { toCurrency, expectedRate }
                }
            };

            _mockApi
                .Setup(x => x.GetLatestRateAsync(TestApiKey, fromCurrency, toCurrency))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _service.GetExchangeRateAsync(fromCurrency, toCurrency);

            // Assert
            Assert.AreEqual(expectedRate, result);
            _mockApi.Verify(x => x.GetLatestRateAsync(TestApiKey, fromCurrency, toCurrency), Times.Once);
        }
    }
}

