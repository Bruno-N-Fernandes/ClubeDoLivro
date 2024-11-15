using ClubeDoLivro.Blazor.Application.Security;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClubeDoLivro.Blazor.Application.ClientServices.Interfaces
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthenticationCoreService, AuthenticationCoreService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
			services.AddTransient<AuthorizationMessageHandler>();

			return services;
		}
	}
}