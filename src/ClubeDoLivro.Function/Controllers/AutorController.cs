using ClubeDoLivro.Function.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Entity = ClubeDoLivro.Domains.Autor;

namespace ClubeDoLivro.Function.Controllers
{
	public class AutorController : AuthController<Entity>
	{
		private const string EntityName = "Autor";
		public AutorController(IServiceProvider serviceProvider) : base(serviceProvider) { }

		[Function(EntityName + "GetAll")]
		[OpenApiOperation(EntityName + "GetAll", EntityName, Summary = "Lista todos os " + EntityName, Description = "Use para Listar todos os " + EntityName)]
		[OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, BearerFormat = "JWT", Description = "Authorization Bearer JsonWebToken", In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = EntityName)] HttpRequestData httpRequestData)
		{
			return await CreateResponse(httpRequestData, user => Service.ObterTodos());
		}

		[Function(EntityName + "GetOne")]
		[OpenApiOperation(EntityName + "GetOne", EntityName, Summary = "Obtém um " + EntityName, Description = "Use para Obter um " + EntityName)]
		[OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, BearerFormat = "JWT", Description = "Authorization Bearer JsonWebToken", In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<HttpResponseData> GetOne([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = EntityName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			return await CreateResponse(httpRequestData, user => Service.ObterPor(id));
		}

		[Function(EntityName + "Create")]
		[OpenApiOperation(EntityName + "Create", EntityName, Summary = "Cria um novo " + EntityName, Description = "Use para Criar um novo " + EntityName)]
		[OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, BearerFormat = "JWT", Description = "Authorization Bearer JsonWebToken", In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + EntityName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<HttpResponseData> Create([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = EntityName)] HttpRequestData httpRequestData)
		{
			return await CreateResponse(httpRequestData, async (user) =>
			{
				var entity = await GetFromBody<Entity>(httpRequestData);
				return await Service.Incluir(entity);
			});
		}

		[Function(EntityName + "Update")]
		[OpenApiOperation(EntityName + "Update", EntityName, Summary = "Atualiza um " + EntityName, Description = "Use para Atualizar um " + EntityName)]
		[OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, BearerFormat = "JWT", Description = "Authorization Bearer JsonWebToken", In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + EntityName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> Update([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = EntityName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			return await CreateResponse(httpRequestData, async (user) =>
			{
				var entity = await GetFromBody<Entity>(httpRequestData);
				if (entity.Id != id)
					throw new Exception("Id informado no path é diferente do id do objeto enviado no Body");

				return await Service.Alterar(entity);
			});
		}

		[Function(EntityName + "Delete")]
		[OpenApiOperation(EntityName + "Delete", EntityName, Summary = "Apaga um " + EntityName, Description = "Use para Apagar um " + EntityName)]
		[OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, BearerFormat = "JWT", Description = "Authorization Bearer JsonWebToken", In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + EntityName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = EntityName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			return await CreateResponse(httpRequestData, async (user) =>
			{
				var entity = new Entity { Id = id };
				return await Service.Excluir(entity);
			});
		}
	}
}