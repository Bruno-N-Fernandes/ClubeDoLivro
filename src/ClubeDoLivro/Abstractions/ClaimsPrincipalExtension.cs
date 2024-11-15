using ClubeDoLivro.Domains;
using ClubeDoLivro.Services;
using System.Security.Claims;

namespace ClubeDoLivro.Abstractions
{
	public static class ClaimsPrincipalExtension
	{
		public static string GetSid(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(IJwtService.Sid)?.Value;
		public static string GetName(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(IJwtService.Name)?.Value;
		public static string GetEmail(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(IJwtService.Email)?.Value;
		public static string GetMobilePhone(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(IJwtService.MobilePhone)?.Value;
		public static string GetExpiration(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(IJwtService.Expiration)?.Value;
		public static Usuario GetUserInfo(this ClaimsPrincipal claimsPrincipal) => IJwtService.GetUserInfo(claimsPrincipal.Claims);
	}
}