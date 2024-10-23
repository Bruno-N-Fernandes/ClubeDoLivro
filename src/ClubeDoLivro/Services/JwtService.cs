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
    public interface IJwtService
	{
		AccessToken GerarJwtToken(Usuario usuario);
	}

	public class JwtService : IJwtService
	{
		private readonly SigningCredentials _signingCredentials;
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

		public JwtService()
		{
			var symetricKeyAuthSecret = "4KygPU/LOcJ8sGHLwN9QLaATWbzaE+GxqNn3YppPo4Mv1zN6Kl/8E6dDq5B0SLoo9uZBJKUnfScqsxPUNYRDTg==";
			var symetricKey = Convert.FromBase64String(symetricKeyAuthSecret);
			var symmetricSecurityKey = new SymmetricSecurityKey(symetricKey);
			_signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
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
			yield return new Claim(ClaimTypes.Sid, usuario.Id.ToString());
			yield return new Claim(ClaimTypes.GivenName, usuario.Nome.ToString());
			yield return new Claim(ClaimTypes.Email, usuario.EMail.ToString());
			yield return new Claim(ClaimTypes.MobilePhone, usuario.Telefone.ToString());
			yield return new Claim(ClaimTypes.Expiration, expiresIn.ToString());
			yield return new Claim("issuedAt", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"));
		}

		private Dictionary<string, object> ToDic(IEnumerable<Claim> claims)
		{
			return claims.ToDictionary<Claim, string, object>(c => c.Type, c => c.Value);
		}
	}
}