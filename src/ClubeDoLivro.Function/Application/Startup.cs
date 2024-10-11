using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Function.Application;
using ClubeDoLivro.Repositories;
using ClubeDoLivro.Services;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;
using System.IO;
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
			services.AddTransient<IDbConnection>(sp => new SqliteConnection(sp.GetService<IConfiguration>().GetConnectionString("ClubeDoLivro")));
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
			//services.AddSingleton(new SecurityInfo { ApplicationName = "Clube do Livro" });

			services.AddTransient<IService<Autor>, AutorService>();
			services.AddTransient<IRepository<Autor>, AutorRepository>();

			services.AddTransient<IService<Livro>, LivroService>();
			services.AddTransient<IRepository<Livro>, LivroRepository>();

			services.AddSingleton<IQueryBuilder, QueryBuilder>();

			//services.AddTransient<JwtService>();

			return services;
		}
	}
}