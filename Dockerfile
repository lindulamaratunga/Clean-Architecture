# Build stage
FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src

COPY ["Money.Api/Money.Api.csproj", "Money.Api/"]
COPY ["Money.Application/Money.Application.csproj", "Money.Application/"]
COPY ["Money.Domain/Money.Domain.csproj", "Money.Domain/"]
COPY ["Money.Infrastructure/Money.Infrastructure.csproj", "Money.Infrastructure/"]
RUN dotnet restore "Money.Api/Money.Api.csproj"

COPY . .
WORKDIR /src/Money.Api
RUN dotnet publish "Money.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS final
WORKDIR /app

EXPOSE 8080

# SQLite and Serilog write outside the published binaries so a volume can keep the database.
ENV ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__DefaultConnection="Data Source=/data/Money.db"

COPY --from=build /app/publish .

RUN mkdir -p /data /app/logs \
    && chmod 777 /data /app/logs

VOLUME ["/data"]

USER $APP_UID

ENTRYPOINT ["dotnet", "Money.Api.dll"]
