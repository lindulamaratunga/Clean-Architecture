using Microsoft.EntityFrameworkCore;
using Money.Api;
using Money.Application.ExternalApi;
using Money.Infrastructure.Data;
using Refit;
using Serilog;

// Create the WebApplication builder, which sets up the host and configuration
var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("logs/money-error-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// with Serilog for enhanced logging capabilities, Use Serilog as the logging provider, replacing the default .NET logger
builder.Host.UseSerilog();

// Add controllers with NewtonsoftJson for JSON serialization
builder.Services.AddControllers().AddNewtonsoftJson();

// Configure Entity Framework Core with SQL Server
//builder.Services.AddDbContext<MoneyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<MoneyDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(CurrencyMappingProfile));

// Add application services
builder.Services.AddApplicationServices();

// Add Refit for Open Exchange Rates API
builder.Services.AddRefitClient<IOpenExchangeRatesApi>()
    .ConfigureHttpClient(c =>
    {
        var baseUrl = builder.Configuration["OpenExchangeRates:BaseUrl"];
        c.BaseAddress = new Uri(baseUrl ?? "https://openexchangerates.org/api");
    });

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with XML comments
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Lindul Amaratunga - Money API",
        Version = "v1",
        Description = "Currency conversion"
    });

    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add Problem Details
builder.Services.AddProblemDetails();

// Build the WebApplication
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoneyDbContext>();
    context.Database.EnsureCreated();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Use authorization middleware
app.UseAuthorization();

//maps all the controller endpoints
app.MapControllers();

// Add global exception handling
app.UseExceptionHandler("/error");

// Define the error handling endpoint
app.MapGet("/error", () => Results.Problem("An error occurred.")).ExcludeFromDescription();

// Run the application
app.Run();
