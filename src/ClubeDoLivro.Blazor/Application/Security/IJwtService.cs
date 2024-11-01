using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClubeDoLivro.Blazor.Application.Security
{
	public interface IJwtService
	{
		const string cIssuedAt = ClaimTypes.UserData + "/issuedAt";
		const string cTenantId = ClaimTypes.UserData + "/tenantId";
		const string cTenantName = ClaimTypes.UserData + "/tenantName";

		const string cAccessToken = "access_token";
		const string cExpiryDate = "expiry_date";
		const string cAuthenticationType = "Bearer";
		const string cAuthorizationHeaderName = "authorization";

		public static readonly AuthenticationState Anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
	}
}