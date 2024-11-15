using ClubeDoLivro.Domains;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Linq;

namespace ClubeDoLivro.Services
{
	public interface IJwtService
	{
		const string Sid = "#" + ClaimTypes.Sid;
		const string Role = "#" + ClaimTypes.Role;
		const string Name = "#" + ClaimTypes.Name;
		const string Email = "#" + ClaimTypes.Email;
		const string MobilePhone = "#" + ClaimTypes.MobilePhone;
		const string Expiration = "#" + ClaimTypes.Expiration;

		const string cAccessToken = "access_token";
		const string cExpiryDate = "expiry_date";
		const string cAuthenticationType = "Bearer";
		const string cAuthorizationHeaderName = "authorization";

		public static string GenerateNewSymmetricSecretKey() => Convert.ToBase64String(new HMACSHA256().Key);

		AccessToken GerarJwtToken(Usuario usuario);
		JwtToken GetAccessToken(string authorizationHeader);

		public static Usuario GetUserInfo(IEnumerable<Claim> claims) => claims?.Any() == true ? GetUserCore(claims) : Usuario.Anonymous;

		private static Usuario GetUserCore(IEnumerable<Claim> claims)
		{
			return new Usuario
			{
				Id = Get(claims, IJwtService.Sid, int.Parse),
				Nome = Get(claims, IJwtService.Name),
				EMail = Get(claims, IJwtService.Email),
				Telefone = Get(claims, IJwtService.MobilePhone),
			};
		}

		public static IDictionary<string, object> Deserialize(IEnumerable<Claim> claims, string prefix)
		{
			return claims.Where(c => c.Type.StartsWith(prefix))
				.ToDictionary(c => c.Type.Substring(prefix.Length), c => (object)c.Value);
		}

		public static TValue Get<TValue>(IEnumerable<Claim> claims, string claimType, Func<string, TValue> selector)
			=> selector.Invoke(Get(claims, claimType));

		public static string Get(IEnumerable<Claim> claims, string claimType)
		   => claims?.SingleOrDefault(c => c.Type == claimType)?.Value;
	}
}