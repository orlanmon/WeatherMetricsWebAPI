//  Dependency Injection

builder.Services.AddControllers();

builder.Services.AddScoped<IWeatherMetricsDataService, WeatherMetricsDataService>(); // Register your service


// Add Swagger Support

Install-Package Swashbuckle.AspNetCore -Version 6.6.2


// Add CORS Support

Install-Package Microsoft.AspNetCore.Cors

!!!!Also remember to Configure the Azure App Service For the Proper CORS Domain Information!!!

Azure: App Service -> API -> CORS  -> Allowed Origins



//  Azure SQL Server Database

Logical SQL Server with an Azure SQL Database



// Connection String
Server=tcp:monacos-sql.database.windows.net,1433;Initial Catalog=WeatherMetricsData;Persist Security Info=False;User ID=orlanmon;Password=GoWestYoungMan_1973;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;



// Add Logging File For Logging

// Side Note Splunk would be the perfect Option in a Large Enterprise 

// Azure App Services logging

Microsoft.Extensions.Logging.AzureAppServices

For the logs to appear in Azure, you must enable the "Application Logging" setting in your App Service. 

Navigate to your App Service in the Azure portal.
Under the Monitoring section in the left menu, select App Service logs.
Toggle Application Logging (Filesystem) to On (this is for temporary debugging and lasts 12 hours) or enable Application Logging (Blob) for long-term storage, specifying a storage account.

You can then view the logs in real-time by selecting the Log stream option under Monitoring in the Azure portal. 





// Deployment of Web Service as Azure App Service 

DEV Slot URI

https://weathermetricswebapi-dev-b8g6a2ecfbewhxcu.canadaeast-01.azurewebsites.net/swagger/index.html



Default Production Slot URI ( slot created automaically )

https://weathermetricswebapi-e4gvgrg3g3cncgd7.canadaeast-01.azurewebsites.net/swagger/index.html



// Add Keyvault Support to App Service Using CLI


https://learn.microsoft.com/en-us/azure/key-vault/general/quick-create-portal


// Deployment of Web Service to Azure App Service with Azure DevOps


https://dev.azure.com/monacos







// YAML

# ASP.NET Core (.NET Framework)
# Build and test ASP.NET Core projects targeting the full .NET Framework.
# Add steps that publish symbols, save build artifacts, and more:
# https://docs.microsoft.com/azure/devops/pipelines/languages/dotnet-core

trigger:
- master

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:DesktopBuildPackageLocation="$(build.artifactStagingDirectory)\WebApp.zip" /p:DeployIisAppPath="Default Web Site"'
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: VSTest@2
  inputs:
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'
- task: AzureRmWebAppDeployment@5
  inputs:
    ConnectionType: 'AzureRM'
    azureSubscription: 'Pay-As-You-Go(0dc78152-04ff-4995-9813-29db62f1aab2)'
    appType: 'webApp'
    WebAppName: 'WeatherMetricsWebAPI'
    deployToSlotOrASE: true
    ResourceGroupName: 'monacosenterprise'
    SlotName: 'production'
    packageForLinux: '$(build.artifactStagingDirectory)\WebApp.zip'
    #packageForLinux: '$(System.DefaultWorkingDirectory)/**/*.zip'












    //  Azure DevOps Steps



    #1 - Create Project 

    MonacosWeatherMetricsWebAPI

    #2 - Create Pipeline  - Select Code Source

    #3 - Create Pipeline  - Select ASP.NET Core (.NET Framework) -.  Generate pipeline YAML.

    #4   Add PipeLine YAML Deployment to App Service - Show Assitant(left hand side)

         App Service Type -  Web App on Windows
         App Service Name -  WeatherMetricsWebAPI
         Deploy to Slot or App Service Environment - Resource Group - monacosenterprise, Slot - Production
         Package of Folder - $(build.artifactStagingDirectory)\WebApp.zip





