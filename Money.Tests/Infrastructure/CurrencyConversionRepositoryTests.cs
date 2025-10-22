using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Domain.Models;
using Money.Infrastructure.Data;
using Money.Infrastructure.Repositories;

namespace Money.Tests.Infrastructure
{
    [TestClass]
    public class CurrencyConversionRepositoryTests
    {
        private MoneyDbContext _context;
        private CurrencyConversionRepository _repository;
        private IFixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();

            // Create in-memory database with a unique name for each test
            var options = new DbContextOptionsBuilder<MoneyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new MoneyDbContext(options);
            _repository = new CurrencyConversionRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddAsync_ValidConversion_AddsToDatabase()
        {
            // Arrange
            var conversion = _fixture.Build<CurrencyConversion>()
                .Without(x => x.Id) // Let database generate ID
                .With(x => x.FromCurrency, "USD")
                .With(x => x.ToCurrency, "EUR")
                .With(x => x.Amount, 10m)
                .With(x => x.ConvertedAmount, 8.5783m)
                .With(x => x.ExchangeRate, 0.857829m)
                .With(x => x.DepartmentId, 1)
                .With(x => x.ConversionDate, DateTime.UtcNow)
                .Create();

            // Act
            var result = await _repository.AddAsync(conversion);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
            Assert.AreEqual("USD", result.FromCurrency);
            Assert.AreEqual("EUR", result.ToCurrency);
            Assert.AreEqual(10m, result.Amount);
            Assert.AreEqual(8.5783m, result.ConvertedAmount);

            // Verify it's in the database
            var savedConversion = await _context.CurrencyConversions.FindAsync(result.Id);
            Assert.IsNotNull(savedConversion);
            Assert.AreEqual(result.Id, savedConversion.Id);
        }
    }
}

