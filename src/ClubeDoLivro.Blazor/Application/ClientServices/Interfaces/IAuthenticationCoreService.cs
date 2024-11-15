using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClubeDoLivro.Blazor.Application.ClientServices.Interfaces
{
	public interface IAuthenticationCoreService
	{
		Task SetAuthorization(HttpRequestHeaders headers);
		Task<DateTime> GetExpiryDate();
		Task<ClaimsPrincipal> GetLoggedUser();
		Task<AuthenticationState> GetAuthenticationState();
	}
}