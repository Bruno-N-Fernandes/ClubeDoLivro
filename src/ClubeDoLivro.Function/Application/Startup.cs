using ClubeDoLivro.Function.Application;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDoLivro.Function.Application
{
	public static class Startup
	{
		public static async Task Main(string[] args)
		{
			var hostBuilder = new HostBuilder();

			hostBuilder.ConfigureAppConfiguration(configurationBuilder =>
			{
				configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
				configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				configurationBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
				configurationBuilder.AddEnvironmentVariables();
			});

			hostBuilder.ConfigureFunctionsWorkerDefaults(configure: builder =>
			{
				//builder.UseExceptionHandlingMiddleware();
				//builder.UseFunctionContextAccessorMiddleware();
				//builder.UseAuthenticationMiddleware();
				//builder.UseAuthorizationMiddleware();
				//builder.UseRefreshTokenMiddleware();
			});

			hostBuilder.ConfigureServices(services =>
			{
				services.AddLogging();
				services.AddSingleton<ILoggerFactory, LoggerFactory>();
				services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("Azure Function"));
				//services.UseFunctionContextAccessorMiddleware();
				//services.UseJwtTokenService();
				services.ConfigureDbConnection();
				services.ConfigureServices();
			});

			using var host = hostBuilder.Build();

			await host.RunAsync();
		}

		public static void ConfigureDbConnection(this IServiceCollection services)
		{
			services.AddTransient<IDbConnection>(sp => new SqlConnection(sp.GetService<IConfiguration>().GetConnectionString("")));
		}

		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			//services.ConfigureJson();
			services.AddSingleton<IOpenApiConfigurationOptions, ApiOptions>();

			services.AddHttpClient();

			//services.AddSingleton<IJwtService, JwtService>();
			//services.AddSingleton<IFormatProviders, FormatProviders>();
			//services.AddSingleton<FormatProviders>();
			//services.AddSingleton(new SecurityInfo { ApplicationName = "Pleno Investidor" });

			//services.AddSingleton<UsuarioQuery>();
			//services.AddTransient<UsuarioRepository>();
			//services.AddTransient<UsuarioService>();

			//services.AddTransient<JwtService>();

			return services;
		}
	}
}