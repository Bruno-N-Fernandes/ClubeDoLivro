using ClubeDoLivro.Abstractions.Interfaces;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Services;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ClubeDoLivro.Function.Abstractions
{
	public abstract class AbstractController<TEntity>
	{
		protected readonly IServiceProvider ServiceProvider;
		protected readonly IService<TEntity> Service;
		protected readonly ILogger Logger;

		protected TService GetService<TService>() => ServiceProvider.GetRequiredService<TService>();

		protected AbstractController(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
			Service = GetService<IService<TEntity>>();
			Logger = GetService<ILogger>();
		}

		protected TValue GetValueFromHeader<TValue>(HttpRequestData httpRequestData, string headerName) => httpRequestData.GetValueFromHeader<TValue>(headerName);

		protected async Task<TValue> GetFromBody<TValue>(HttpRequestData httpRequestData) => await httpRequestData.GetObjectFromBody<TValue>();
	}

	public abstract class AuthController<TEntity> : AbstractController<TEntity>
	{
		protected IJwtService JwtService => GetService<IJwtService>();

		protected AuthController(IServiceProvider serviceProvider) : base(serviceProvider) { }

		protected async Task<Usuario> GetCurrentUser(HttpRequestData request)
		{
			if (request == null || !request.Headers.TryGetValues(IJwtService.cAuthorizationHeaderName, out var authorizationHeader) || string.IsNullOrWhiteSpace(authorizationHeader?.FirstOrDefault()))
				throw new AuthenticationException("No Authorization header was present");

			var jwtToken = JwtService.GetAccessToken(authorizationHeader?.FirstOrDefault());
			if (!jwtToken.IsValid)
				throw new AuthenticationException("No Valid Token was present");

			if (jwtToken.HasExpired)
				throw new AuthenticationException($"Token Expired: {jwtToken.ExpiresAt}");

			if (jwtToken.Usuario is null)
				throw new AuthenticationException("No identity key was found in the claims.");

			return await Task.FromResult(jwtToken.Usuario);
		}

		protected async Task<HttpResponseData> CreateResponse<TResult>(HttpRequestData httpRequestData, Func<Usuario, Task<TResult>> function)
		{
			try
			{
				var user = await GetCurrentUser(httpRequestData);
				var result = function.Invoke(user);
				return await httpRequestData.OkResponse(result);
			}
			catch (AuthenticationException exception)
			{
				return await httpRequestData.UnauthorizedResponse(new Message(exception.Message));
			}
			catch (Exception exception)
			{
				return await httpRequestData.BadRequestResponse(new Message(exception.Message));
			}
		}
	}
}