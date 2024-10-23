using ClubeDoLivro.Domains;
using ClubeDoLivro.Function.Abstractions;
using ClubeDoLivro.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Entity = ClubeDoLivro.Domains.Usuario;

namespace ClubeDoLivro.Function.Controllers.Security
{
	/// <summary>
	/// https://joonasw.net/view/azure-ad-jwt-authentication-in-net-isolated-process-azure-functions
	/// </summary>
	public class LoginController : AbstractController<Entity>
	{
		private const string ModelName = "Login";
		private readonly UsuarioService UsuarioService;

		public LoginController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			UsuarioService = (UsuarioService)Service;
		}


		[Function(ModelName + "Authenticate")]
		[OpenApiOperation(ModelName + "Authenticate", ModelName, Summary = "Use para se autenticar", Description = "Use para se autenticar pelo EMail e Senha e obter um Token")]
		[OpenApiRequestBody("application/json", typeof(LoginRequest), Required = true, Description = ModelName + " Json")]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(AccessToken), Description = "OK response")]
		public async Task<HttpResponseData> Authenticate([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = ModelName + "/Authenticate")] HttpRequestData httpRequestData)
		{
			try
			{
				var loginRequest = await GetFromBody<LoginRequest>(httpRequestData);

				var token = await UsuarioService.EfetuarLogin(loginRequest);

				return await httpRequestData.OkResponse(token);
			}
			catch (Exception exception)
			{
				return await httpRequestData.BadRequestResponse(new Message(exception.Message));
			}
		}

		[Function(ModelName + "Create")]
		[OpenApiOperation(ModelName + "Create", ModelName, Summary = "Use para criar um " + ModelName, Description = "Use para criar um " + ModelName)]
		[OpenApiRequestBody("application/json", typeof(Entity), Required = true, Description = ModelName + " Json")]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<HttpResponseData> Create([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = ModelName + "/Create")] HttpRequestData httpRequestData)
		{
			var usuario = await GetFromBody<Entity>(httpRequestData);
			await Service.Incluir(usuario);
			return await httpRequestData.CreatedResponse($"/{usuario.Id}", usuario);
		}
	}
}