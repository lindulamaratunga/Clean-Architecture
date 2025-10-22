# Currency Converter Web API

A comprehensive .NET 9 Web API for currency conversion using Open Exchange Rates API, built with clean architecture principles and modern development practices.

## 🚀 Features

- **Real-time Currency Conversion**: Integrates with Open Exchange Rates API for live exchange rates
- **Clean Architecture**: Implements SOLID principles with Repository and Service patterns
- **Comprehensive Testing**: Unit tests using MSTest2, AutoFixture, and Moq
- **Database Integration**: Entity Framework Core with code-first migrations
- **API Documentation**: Swagger/OpenAPI integration with detailed documentation
- **Error Handling**: ProblemDetails for consistent error responses
- **Resilience**: Polly policies for retry and circuit breaker patterns
- **Logging**: Structured logging with Serilog
- **Dependency Injection**: Full DI container configuration

## 🏗️ Architecture

The project follows Clean Architecture principles with the following layers:

```
Money/
├── Controllers/          # API Controllers
├── Services/            # Business Logic Services
├── Repositories/        # Data Access Layer
├── Factories/           # Factory Pattern Implementation
├── Models/              # Entity Models
├── DTOs/                # Data Transfer Objects
├── Mappings/            # AutoMapper Profiles
├── Data/                # Entity Framework DbContext
├── External/            # External API Integration (Refit)
└── Money.Tests/ # Unit Test Project
```

## 🛠️ Technologies Used

### Core Technologies
- **.NET 9** - Latest .NET framework
- **ASP.NET Core Web API** - Web API framework
- **Entity Framework Core** - ORM for database operations
- **AutoMapper** - Object-to-object mapping
- **Refit** - Type-safe HTTP client for Open Exchange Rates API

### Testing Frameworks
- **MSTest2** - Unit testing framework
- **AutoFixture** - Test data generation
- **Moq** - Mocking framework
- **FluentAssertions** - Fluent assertion library

### Additional Libraries
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI documentation
- **Serilog** - Structured logging
- **Polly** - Resilience and transient-fault-handling library
- **Microsoft.Extensions.Http.Polly** - HTTP resilience policies

## 📋 Prerequisites

- Visual Studio 2022 (Community/Professional/Enterprise)
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Postman (for API testing)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd Money
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```

### 3. Update Configuration
Update `appsettings.json` with your database connection string if needed:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CurrencyConverterDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "OpenExchangeRates": {
    "ApiKey": "041f13216f7840fbbeb5d9828390ce51",
    "BaseUrl": "https://openexchangerates.org/api"
  }
}
```

### 4. Run the Application
```bash
dotnet run --project Money
```

The API will be available at:
- **HTTPS**: `https://localhost:7001`
- **HTTP**: `http://localhost:5000`
- **Swagger UI**: `https://localhost:7001` (root URL)

### 5. Run Tests
```bash
dotnet test
```

## 📚 API Endpoints

### Currency Conversion
- **POST** `/api/currency/convert` - Convert currency from one to another

### Currency Management
- **GET** `/api/currency` - Get all supported currencies

### Conversion History
- **GET** `/api/currency/history` - Get conversion history (optional userId filter)
- **GET** `/api/currency/{id}` - Get specific conversion by ID

## 📖 API Usage Examples

### Convert Currency
```bash
curl -X POST "https://localhost:7001/api/currency/convert" \
  -H "Content-Type: application/json" \
  -d '{
    "fromCurrency": "USD",
    "toCurrency": "CAD",
    "amount": 100.00,
    "userId": "user-123"
  }'
```

### Get All Currencies
```bash
curl -X GET "https://localhost:7001/api/currency"
```

## 🧪 Testing

### Running Unit Tests
```bash
dotnet test Money.Tests
```

### Test Coverage
The test suite includes:
- **Service Layer Tests**: Business logic validation
- **Controller Tests**: API endpoint testing
- **Factory Tests**: Factory pattern implementation
- **Integration Tests**: End-to-end scenarios

### Additional Test Suggestions
See `Money.Tests/AdditionalTestSuggestions.md` for comprehensive test coverage recommendations.

## 📦 Postman Collection

Import the provided `CurrencyConverterAPI.postman_collection.json` into Postman for comprehensive API testing. The collection includes:
- Currency conversion examples
- Error handling scenarios
- Validation tests
- Automated test assertions

## 🔧 Configuration

### Environment Variables
- `ConnectionStrings__DefaultConnection` - Database connection string
- `OpenExchangeRates__ApiKey` - Open Exchange Rates API key
- `OpenExchangeRates__BaseUrl` - Open Exchange Rates base URL

### Logging
Logs are written to the `logs/` directory with daily rotation. Configure Serilog in `Program.cs` for custom logging behavior.

## 🏛️ Design Patterns Implemented

### 1. Repository Pattern
- `ICurrencyRepository` / `CurrencyRepository`
- `ICurrencyConversionRepository` / `CurrencyConversionRepository`

### 2. Service Pattern
- `ICurrencyService` / `CurrencyService`
- `IExchangeRateService` / `ExchangeRateService`

### 3. Factory Pattern
- `ICurrencyConverterFactory` / `CurrencyConverterFactory`

### 4. Dependency Injection
- Full DI container configuration in `Program.cs`
- Interface-based dependency registration

### 5. AutoMapper
- `CurrencyMappingProfile` for object mapping
- DTO to Entity and Entity to DTO transformations

## 🔒 Security Considerations

- API key stored in configuration (consider Azure Key Vault for production)
- Input validation on all endpoints
- SQL injection prevention through Entity Framework
- CORS configuration (add as needed)

## 🚀 Deployment

### Local Development
```bash
dotnet run
```

### Production Deployment
1. Update connection strings for production database
2. Configure proper logging levels
3. Set up monitoring and health checks
4. Configure SSL certificates
5. Set up CI/CD pipeline

## 📊 Performance Considerations

- **Caching**: Consider implementing Redis for exchange rate caching
- **Rate Limiting**: Implement rate limiting for API endpoints
- **Database Optimization**: Add indexes for frequently queried fields
- **Async/Await**: All database operations are asynchronous

## 🔄 Future Enhancements

- [ ] Add authentication and authorization
- [ ] Implement caching for exchange rates
- [ ] Add health checks
- [ ] Implement rate limiting
- [ ] Add monitoring and metrics
- [ ] Support for more currency providers
- [ ] Historical exchange rate data
- [ ] Bulk currency conversion

## 📝 Code Quality

The project follows:
- **SOLID Principles**: Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
- **DRY Principle**: Don't Repeat Yourself
- **Clean Code**: Meaningful names, small functions, clear structure
- **Async/Await**: Non-blocking operations throughout

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

## 📄 License

This project is created for technical assessment purposes.

## 📞 Support

For questions or issues, please refer to the code documentation or create an issue in the repository.

---

**Built with ❤️ using .NET 9 and modern development practices**
