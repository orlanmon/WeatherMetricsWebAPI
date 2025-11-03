// MS Entity Framework

 Install-Package Microsoft.EntityFrameworkCore.SqlServer 
 Install-Package Microsoft.EntityFrameworkCore.Design 
 Install-Package Microsoft.EntityFrameworkCore.Tools 


 Command Line On Project

 dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
 dotnet add package Microsoft.EntityFrameworkCore.Design 
 dotnet add package Microsoft.EntityFrameworkCore.Tools
 dotnet tool update --global dotnet-ef


 // Scaffolding

 https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli


 

 //  Database  


 WeatherMetricsData

 


 /****** Object:  Table [dbo].[WeatherMetricsLog]    Script Date: 11/3/2025 11:33:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WeatherMetricsLog](
	[WeatherMetricsLog_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BarometricPressure] [int] NULL,
	[Humidity] [int] NULL,
	[TemperatureCelsius] [int] NULL,
	[WindSpeed] [int] NULL,
	[WindDirection] [nvarchar](2) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[EntryDate] [datetime] NULL,
 CONSTRAINT [PK_WeatherMetricsLog] PRIMARY KEY CLUSTERED 
(
	[WeatherMetricsLog_ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WeatherMetricsLog] ADD  CONSTRAINT [DF_WeatherMetricsLog_EntryDate]  DEFAULT (getdate()) FOR [EntryDate]
GO


use master

/* Create Server Level User */

CREATE LOGIN [orlanmon] WITH PASSWORD='GoWestYoungMan_1973', DEFAULT_DATABASE=[WeatherMetricsData], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [orlanmon] ENABLE
GO


use WeatherMetricsData

CREATE USER orlanmon FOR LOGIN orlanmon
    WITH DEFAULT_SCHEMA = dbo;
GO



dotnet ef dbcontext scaffold "Server=HAL9000;Database=WeatherMetricsData;Trusted_Connection=False; User ID=sa;Password=GoWestYoungMan_1972; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c WeatherMetricsDbContext



 // DB Context Revise

  private readonly string _connectionString;

  public WeatherMetricsDbContext(string connectionString)
    {

        _connectionString = connectionString;

    }