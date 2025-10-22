## C#.NET Core – Technical Test
## Lindul Amaratunga - Money API

## 🎯 Project Overview
## Currency conversion

Objective
Design and create a Web API project that will connect with a 3rd party foreign exchange data provider (Open Exchange Rates)
to convert a dollar amount from one currency to another based on the currency type requested (ie. CDN or USD). 
Converted amounts and currency types should be saved to the database and be filter via the API.


## 🏗️ Architecture
✅ AutoMapper
✅ Dependency Injection (DI)
✅ Factory pattern
✅ DRY, SOLID, Clean Architecture, Service/Repository patterns
✅ AutoFixture & Mock
✅ Code-first migration
✅ Entity Framework (EF) 
✅ LINQ
✅ Refit
✅ RESTful API
✅ Swagger
✅ ProblemDetails


## Architecture Diagram
```
Money.Api/              API Layer
 └── Controllers/            ✅ API Controllers

Money.Application/      Application Layer
 ├── ExternalDTOs/           ✅ Data Transfer Objects (Request and Response Models)
 ├── External/               ✅ External API Integration (Interfaces and Implementations)
 ├── Factories/              ✅ Factory Pattern Design (Interfaces and Implementations)
 ├── ServiceDTOs/            ✅ Data Transfer Objects (Request and Response Models)
 └── Services/               ✅ Business Logics (Interfaces and Implementations)

Money.Domain/           Domain Layer
 ├── Models/                 ✅ Entity Models
 └── Repositories/           ✅ Repository Interfaces (Abstractions)

Money.Infrastructure/   Infrastructure Layer
 ├── Data/                   ✅ Entity Framework DbContext
 └── Repositories/           ✅ Repository Implementations (Implementations))   

Money.Tests/            Unit Test Project
```


### Currency Conversion
- POST `/api/CurrencyConversion/convert` - Convert currency from one to another

### Filter Conversion History
- GET `/api/CurrencyConversion/get-by-department/{Id}` - Get conversion history by department ID

### Filter Conversion History
- GET `/api/CurrencyConversion/get-by-id/{id}` - Get conversion history by ID

### Conversion History
- GET `/api/CurrencyConversion/get-All` - Get conversion history
- 


## 🛠️ Technologies Used
✅ .NET framework 9.0
✅ Entity Framework Code First
✅ ASP.NET Core Web API


## 🛠️ Database
✅ Sqlite (Easily switchable to SQL Server)


## 🔧 Configuration
Visual Studio 2022 (Minimum - Community/Free)
Use OpenExchangeRates as your API EndPoint (https://docs.openexchangerates.org/docs/latest-json)
API Key: bb1fa95781614db0b5c178a986d5b1de
  

## 🔧 Appsettings.json
appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=CurrencyConverter.db"
  },
  "OpenExchangeRates": {
    "ApiKey": "bb1fa95781614db0b5c178a986d5b1de",
    "BaseUrl": "https://openexchangerates.org/api"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


## 🚀 Features
-
✅ Real-time Currency Conversion OpenExchangeRates
✅ Swagger Integration
✅ Clean Architecture with modern development practices
✅ Clean and readable code
✅ Scalable and maintainable structure
✅ Database Integration
✅ API Documentation
✅ Error Handling with ProblemDetails and Error logging
✅ Comprehensive Testing
-


### Testing Frameworks
✅ MSTest2 - Unit testing framework
✅ AutoFixture - Test data generation
✅ Moq - Mocking framework
✅ FluentAssertions - Fluent assertion library



