using Blazored.LocalStorage;
using ClubeDoLivro.Blazor.Application.ClientServices.Interfaces;
using ClubeDoLivro.Blazor.Application.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using System.Net.Http.Headers;

namespace ClubeDoLivro.Blazor
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddMudServices(options =>
			{
				options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
			});

			builder.Services.AddHttpClient("Web", client =>
			{
				var baseAddressWeb = builder.HostEnvironment.BaseAddress;
				client.BaseAddress = new Uri(baseAddressWeb);
			});

			var httpCliente = builder.Services.AddHttpClient("Api", client =>
			{
				var baseAddressApi = builder.Configuration["BaseAddressApi"];
				client.BaseAddress = new Uri(baseAddressApi);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.Timeout = TimeSpan.FromMinutes(1);
			});

			httpCliente.AddHttpMessageHandler<AuthorizationMessageHandler>();
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));
			builder.Services.AddHttpClientServices();
			builder.Services.AddAuthorizationCore();

			await builder.Build().RunAsync();
		}
	}
}
