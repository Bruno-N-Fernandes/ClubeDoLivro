using Blazored.LocalStorage;
using ClubeDoLivro.Blazor.Application.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
			builder.Services.AddMudServices();
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
			builder.Services.AddTransient<AuthorizationMessageHandler>();

			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

			await builder.Build().RunAsync();
		}
	}
}
