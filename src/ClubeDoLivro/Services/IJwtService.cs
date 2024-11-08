using ClubeDoLivro.Domains;
using System.Security.Claims;

namespace ClubeDoLivro.Services
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

		AccessToken GerarJwtToken(Usuario usuario);
		JwtToken GetAccessToken(string authorizationHeader);
	}
}