//  Dependency Injection

builder.Services.AddControllers();

builder.Services.AddScoped<IWeatherMetricsDataService, WeatherMetricsDataService>(); // Register your service


// Add Swagger Support

Install-Package Swashbuckle.AspNetCore -Version 6.6.2


// Add CORS Support

Install-Package Microsoft.AspNetCore.Cors


//  Azure SQL Server Database

// Connection String
Server=tcp:monacos-sql.database.windows.net,1433;Initial Catalog=WeatherMetricsData;Persist Security Info=False;User ID=orlanmon;Password=GoWestYoungMan_1973;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;







// Deployment of Web Service as Azue App Service

DEV Slot

https://weathermetricswebapi-dev-b8g6a2ecfbewhxcu.canadaeast-01.azurewebsites.net/swagger/index.html

