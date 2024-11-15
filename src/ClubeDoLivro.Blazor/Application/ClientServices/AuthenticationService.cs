using ClubeDoLivro.Blazor.Application.ClientServices.Interfaces;
using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Blazor.Pages.Autenticacao;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClubeDoLivro.Blazor.Application.ClientServices
{
	public class AuthenticationService : AuthenticationCoreService, IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public AuthenticationService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HttpClient>();
			_authenticationStateProvider = serviceProvider.GetRequiredService<AuthenticationStateProvider>();
		}

		public async Task<AuthenticationState> Authenticate(LoginRequest loginRequest)
		{
			var response = await _httpClient.PostAsJsonAsync("Login/Authenticate", loginRequest.Criptografar());
			if (response.IsSuccessStatusCode)
			{
				var tokenResponse = await response.Content.ReadFromJsonAsync<AccessToken>();
				return await SetLogin(tokenResponse);
			}
			else
			{
				var apiResponse = await response.Content.ReadFromJsonAsync<Message>();
				throw new ApiException(apiResponse, response.StatusCode);
			}
		}

		public async Task<AuthenticationState> SetLogin(AccessToken tokenResponse)
		{
			await SaveLoginInfo(tokenResponse);
			return await _authenticationStateProvider.GetAuthenticationStateAsync();
		}

		public async Task<AuthenticationState> SetLogout()
		{
			await RemoveLoginInfo();
			return await _authenticationStateProvider.GetAuthenticationStateAsync();
		}
	}
}