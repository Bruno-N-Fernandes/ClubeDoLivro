using System;
using System.Security.Claims;

namespace ClubeDoLivro.Domains
{
	public class AccessToken
	{
		public string Scheme { get; set; }
		public string Token { get; set; }
		public long ExpiresIn { get; set; }
		public DateTime ExpiresAt { get; set; }
	}

	public class JwtToken : AccessToken
	{
		public bool IsValid { get; set; }
		public bool HasExpired { get; set; }
		public ClaimsPrincipal ClaimsPrincipal { get; set; }
		public Usuario Usuario { get; set; }
	}
}