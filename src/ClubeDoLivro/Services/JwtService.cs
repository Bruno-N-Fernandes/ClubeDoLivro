using ClubeDoLivro.Domains;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ClubeDoLivro.Services
{
	public class JwtService : IJwtService
	{
		private const string symetricKeyAuthSecret = "4KygPU/LOcJ8sGHLwN9QLaATWbzaE+GxqNn3YppPo4Mv1zN6Kl/8E6dDq5B0SLoo9uZBJKUnfScqsxPUNYRDTg==";
		private static readonly byte[] symetricKey = Convert.FromBase64String(symetricKeyAuthSecret);
		private static readonly SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(symetricKey);
		private readonly SigningCredentials _signingCredentials;
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
		private readonly TokenValidationParameters ValidationParameters = new() { RequireExpirationTime = true, ValidateIssuer = false, ValidateAudience = false, IssuerSigningKey = _symmetricSecurityKey };

		public JwtService()
		{
			_signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

		public static string GenerateNewSymmetricSecretKey() => Convert.ToBase64String(new HMACSHA256().Key);

		public AccessToken GerarJwtToken(Usuario usuario)
		{
			var now = DateTime.UtcNow;
			var tokenTimeOut = TimeSpan.FromHours(12);
			var claims = GenerateClaims(usuario, Convert.ToInt64(tokenTimeOut.TotalSeconds)).ToArray();
			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				NotBefore = now,
				IssuedAt = now,
				Expires = now.Add(tokenTimeOut),
				SigningCredentials = _signingCredentials,
				Issuer = "ClubeDoLivro",
				Audience = "ClubeDoLivro",
				Claims = ToDic(claims),
			};

			var token = _jwtSecurityTokenHandler.CreateEncodedJwt(securityTokenDescriptor);

			var accessToken = new AccessToken
			{
				Scheme = "Bearer",
				Token = token,
				ExpiresIn = Convert.ToInt64(tokenTimeOut.TotalSeconds),
				ExpiresAt = now.Add(tokenTimeOut),
			};

			return accessToken;
		}

		private IEnumerable<Claim> GenerateClaims(Usuario usuario, long expiresIn)
		{
			yield return new Claim(IJwtService.Sid, usuario.Id.ToString());
			yield return new Claim(IJwtService.Name, usuario.Nome.ToString());
			yield return new Claim(IJwtService.Email, usuario.EMail.ToString());
			yield return new Claim(IJwtService.MobilePhone, usuario.Telefone.ToString());
			yield return new Claim(IJwtService.Expiration, expiresIn.ToString());
		}

		public JwtToken GetAccessToken(string authorizationHeader)
		{
			if (authorizationHeader.StartsWith(IJwtService.cAuthenticationType, StringComparison.OrdinalIgnoreCase))
				authorizationHeader = authorizationHeader[IJwtService.cAuthenticationType.Length..].Trim();

			var now = DateTime.UtcNow;
			var claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(authorizationHeader, ValidationParameters, out var securityToken);

			return new JwtToken
			{
				Scheme = IJwtService.cAuthenticationType,
				Token = authorizationHeader,
				IsValid = (securityToken != null),
				ExpiresAt = securityToken?.ValidTo ?? now,
				HasExpired = (securityToken?.ValidFrom > now) || (now > securityToken?.ValidTo),
				ClaimsPrincipal = claimsPrincipal,
				Usuario = GetUsuario(claimsPrincipal),
			};
		}

		private Usuario GetUsuario(ClaimsPrincipal claimsPrincipal)
		{
			var claims = claimsPrincipal.Claims;
			return new Usuario
			{
				Id = Get(claims, IJwtService.Sid, int.Parse),
				Nome = Get(claims, IJwtService.Name, v => v),
				EMail = Get(claims, IJwtService.Email, v => v),
				Telefone = Get(claims, IJwtService.MobilePhone, v => v),
			};
		}

		private T Get<T>(IEnumerable<Claim> claims, string claimType, Func<string, T> selector)
		{
			var value = claims?.FirstOrDefault(c => c.Type == claimType)?.Value;
			return selector.Invoke(value);
		}

		private Dictionary<string, object> ToDic(IEnumerable<Claim> claims)
		{
			return claims.ToDictionary<Claim, string, object>(c => c.Type, c => c.Value);
		}
	}
}