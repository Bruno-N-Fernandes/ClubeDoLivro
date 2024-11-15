using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClubeDoLivro.Blazor.Application.ClientServices.Interfaces
{
	public interface IAuthenticationService
	{
		Task<AuthenticationState> Authenticate(LoginRequest loginRequest);
		Task<DateTime> GetExpiryDate();
		Task<AuthenticationState> SetLogin(AccessToken tokenResponse);
		Task<AuthenticationState> SetLogout();
	}
}