using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http.Headers;
using System.Net.Http;

namespace ClubeDoLivro.Blazor
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();
			builder.Services.AddHttpClient("Web", client =>
			{
				var baseAddressWeb = builder.HostEnvironment.BaseAddress;
				client.BaseAddress = new Uri(baseAddressWeb);
			}); 
			
			builder.Services.AddHttpClient("Api", client =>
			{
				var baseAddressApi = builder.Configuration["BaseAddressApi"];
				client.BaseAddress = new Uri(baseAddressApi);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.Timeout = TimeSpan.FromMinutes(1);
			});

			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

			await builder.Build().RunAsync();
		}
	}
}
