using Blazored.LocalStorage;
using ClubeDoLivro.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClubeDoLivro.Blazor.Application.Security
{
	public class JwtAuthenticationStateProvider : AuthenticationStateProvider
	{
		private static readonly AuthenticationState Anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		private readonly ILocalStorageService _localStorageService;

		public JwtAuthenticationStateProvider(IServiceProvider serviceProvider)
		{
			_localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			if (await _localStorageService.ContainKeyAsync(IJwtService.cAccessToken))
			{
				var tokenAsString = await _localStorageService.GetItemAsStringAsync(IJwtService.cAccessToken);
				var tokenHandler = new JwtSecurityTokenHandler();

				var token = tokenHandler.ReadJwtToken(tokenAsString);
				var identity = new ClaimsIdentity(token.Claims, IJwtService.cAuthenticationType);
				var user = new ClaimsPrincipal(identity);
				var authState = new AuthenticationState(user);
				NotifyAuthenticationStateChanged(Task.FromResult(authState));
				return authState;
			}

			return Anonymous;
		}
	}
}