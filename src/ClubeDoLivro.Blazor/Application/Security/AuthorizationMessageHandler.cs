using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ClubeDoLivro.Blazor.Application.Security
{
	public class AuthorizationMessageHandler : DelegatingHandler
	{

		private readonly ILocalStorageService _localStorageService;

		public AuthorizationMessageHandler(IServiceProvider serviceProvider)
		{
			_localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (await _localStorageService.ContainKeyAsync(IJwtService.cAccessToken))
			{
				var token = await _localStorageService.GetItemAsStringAsync(IJwtService.cAccessToken);
				request.Headers.Authorization = new AuthenticationHeaderValue(IJwtService.cAuthenticationType, token);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}