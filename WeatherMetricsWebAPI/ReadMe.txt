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


// Notes regarding async await, more reading needs to be done to better understand innerworkings 


The State Machine solves this by introducing a "pause and resume" mechanism. It tracks exactly where a method is paused,
preserves its variables, and frees up the current thread to do other work. 
When the background job completes, the state machine restores the context and resumes executing the rest of the method




// Deployment of Web Service as Azure App Service 


Default Production Slot URI ( slot created automaically )




// Deployment of Web Service to Azure App Service with Azure DevOps


https://dev.azure.com/monacos


// Production App Service End Point:  

https://weathermetricswebapi-e4gvgrg3g3cncgd7.westus3-01.azurewebsites.net


// Test Swagger Interface

https://weathermetricswebapi-e4gvgrg3g3cncgd7.westus3-01.azurewebsites.net/swagger/index.html





// Azure DEV/OPS CI/CD  Pipeline

// Azure DevOps YAML pipeline follows a strict hierarchical nesting system: Trigger → Stages → Jobs → Steps → Tasks
// An Azure YAML pipeline does not require stages or jobs because Azure Pipelines supports implicit structures for simple workflows
// Trigger -> Steps -> Tasks for my Azure Deployments


// Note For Unit Testing!

The VSTest@2 runner does not understand xUnit natively. It relies on a Visual Studio test adapter to find the tests inside your compiled code.
Ensure your xUnit test project .csproj includes the following NuGet packages:
• xunit (The testing framework)
• Microsoft.NET.Test.Sdk (Allows execution)
• xunit.runner.visualstudio



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




// Note Example For YAML with Stages, Jobs, Steps, Task For Future Reference


yaml
trigger:
  - main

stages:
  - stage: Build
    displayName: 'Build Stage'
    jobs:
      - job: BuildJob
        steps:
          - script: echo "Building application..."

  - stage: Test
    displayName: 'Test Stage'
    dependsOn: Build # Waits for Build to finish
    jobs:
      - job: TestJob
        steps:
          - script: echo "Running tests..."

  - stage: Deploy
    displayName: 'Deploy Stage'
    dependsOn: Test # Waits for Test to finish
    jobs:
      - job: DeployJob
        steps:
          - script: echo "Deploying to production..."


