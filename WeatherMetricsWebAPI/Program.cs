
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeatherMetricsDTO;
using WeatherMetricsWebAPI.ServicesImplementation;
using Microsoft.Extensions.Logging.AzureAppServices;


namespace WeatherMetricsWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // IConfiguration is accessible through the builder.Configuration property
            IConfiguration configuration = builder.Configuration;



            // Add services to the container.

            builder.Services.AddControllers();

            // Add Swagger Support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            //builder.Services.AddScoped<IWeatherMetricsDataService, WeatherMetricsDataService>(); // Register your service

            builder.Services.AddScoped<IWeatherMetricsDataService>(provider =>
            {
                
                string? dBConnectionString = configuration.GetConnectionString("DBConnection");
                //string? dBConnectionString = _configuration["ConnectionStrings:DBConnection"];


                return new WeatherMetricsDataService(dBConnectionString);
            });


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            // Conifgure CORS Settings

            builder.Services.AddCors(options =>
            {
                // Configure SpecificOriginsPolicy
                var specificOriginsSection = builder.Configuration.GetSection("CorsSettings:SpecificOriginsPolicy");
                if (specificOriginsSection.Exists())
                {
                    var allowedOrigins = specificOriginsSection["AllowedOrigins"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowedHeaders = specificOriginsSection["AllowedHeaders"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowedMethods = specificOriginsSection["AllowedMethods"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowCredentials = specificOriginsSection.GetValue<bool>("AllowCredentials");

                    options.AddPolicy("SpecificOriginsPolicy", policy =>
                    {
                        if (allowedOrigins != null && allowedOrigins.Length > 0)
                        {
                            policy.WithOrigins(allowedOrigins);
                        }
                        if (allowedHeaders != null && allowedHeaders.Length > 0)
                        {
                            policy.WithHeaders(allowedHeaders);
                        }
                        if (allowedMethods != null && allowedMethods.Length > 0)
                        {
                            policy.WithMethods(allowedMethods);
                        }
                        if (allowCredentials)
                        {
                            policy.AllowCredentials();
                        }
                    });
                }

                // Configure AnyOriginPolicy (if needed)
                /*
                var anyOriginSection = builder.Configuration.GetSection("CorsSettings:AnyOriginPolicy");
                if (anyOriginSection.Exists() && anyOriginSection.GetValue<bool>("AllowAnyOrigin"))
                {
                    options.AddPolicy("AnyOriginPolicy", policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                }
                */

            });



            // Logging

            builder.Services.AddLogging(loggingbuilder =>

                loggingbuilder.ClearProviders()


            );


            // Azure Logging Enabled
            builder.Logging.AddAzureWebAppDiagnostics();

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.MapOpenApi();

                // Add Swagger Support
                app.UseSwagger();
                app.UseSwaggerUI();


            //}

            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers(); // Maps attribute-routed controllers


            app.UseCors("SpecificOriginsPolicy");




            // Configure AutoMapper For Model to DTOs 
            // AutoMapperConfiguration.Configure();


            app.Run();
        }
    }
}
