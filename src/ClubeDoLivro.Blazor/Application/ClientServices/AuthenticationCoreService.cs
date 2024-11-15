using Blazored.LocalStorage;
using ClubeDoLivro.Blazor.Application.ClientServices.Interfaces;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClubeDoLivro.Blazor.Application.ClientServices
{

	public class AuthenticationCoreService : IAuthenticationCoreService
	{
        private static readonly AuthenticationState Anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		private readonly ILocalStorageService _localStorageService;

		public AuthenticationCoreService(IServiceProvider serviceProvider)
		{
			_localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
		}

		protected async Task SaveLoginInfo(AccessToken tokenResponse)
		{
			await _localStorageService.SetItemAsStringAsync(IJwtService.cAccessToken, tokenResponse.Token);
			await _localStorageService.SetItemAsync(IJwtService.cExpiryDate, tokenResponse.ExpiresAt);
		}

		protected async Task RemoveLoginInfo()
		{
			await _localStorageService.RemoveItemAsync(IJwtService.cAccessToken);
			await _localStorageService.RemoveItemAsync(IJwtService.cExpiryDate);
		}

		public async Task SetAuthorization(HttpRequestHeaders headers)
		{
			if (await _localStorageService.ContainKeyAsync(IJwtService.cAccessToken))
			{
				var token = await _localStorageService.GetItemAsStringAsync(IJwtService.cAccessToken);
				headers.Authorization = new AuthenticationHeaderValue(IJwtService.cAuthenticationType, token);
			}
		}

		public async Task<DateTime> GetExpiryDate()
		{
			if (await _localStorageService.ContainKeyAsync(IJwtService.cExpiryDate))
				return await _localStorageService.GetItemAsync<DateTime>(IJwtService.cExpiryDate);
			return DateTime.MinValue.ToUniversalTime();
		}

		public async Task<ClaimsPrincipal> GetLoggedUser()
		{
			if (await _localStorageService.ContainKeyAsync(IJwtService.cAccessToken))
			{
				var tokenAsString = await _localStorageService.GetItemAsStringAsync(IJwtService.cAccessToken);
				var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

				var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(tokenAsString);
				var identity = new ClaimsIdentity(jwtSecurityToken.Claims, IJwtService.cAuthenticationType);
				return new ClaimsPrincipal(identity);
			}

			return null;
		}

		public async Task<AuthenticationState> GetAuthenticationState()
		{
			var claimsPrincipal = await GetLoggedUser();
			return claimsPrincipal is null ? Anonymous : new AuthenticationState(claimsPrincipal);
		}
	}
}