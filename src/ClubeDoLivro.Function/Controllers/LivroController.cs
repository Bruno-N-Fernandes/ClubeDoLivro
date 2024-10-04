using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Function.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Entity = ClubeDoLivro.Domains.Livro;

namespace ClubeDoLivro.Function.Controllers
{
	public class LivroController : AbstractController
	{
		private const string ModelName = "Livro";
		public IService<Entity> Service { get; }

		public LivroController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			Service = GetService<IService<Entity>>();
		}

		[Function(ModelName + "GetAll")]
		[OpenApiOperation(ModelName + "GetAll", ModelName, Summary = "Listar todos os " + ModelName + "(s)", Description = "Use para Listar todos os " + ModelName + "(s)")]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = ModelName)] HttpRequestData httpRequestData)
		{
			try
			{
				return await Service.ObterTodos();
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "GetOne")]
		[OpenApiOperation(ModelName + "GetOne", ModelName, Summary = "Busca o " + ModelName, Description = "Use para Buscar o " + ModelName)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> GetOne([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				return await Service.ObterPor(id);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}


		[Function(ModelName + "Create")]
		[OpenApiOperation(ModelName + "Create", ModelName, Summary = "Cria um novo " + ModelName, Description = "Use para criar um novo " + ModelName)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + ModelName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> Create([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = ModelName)] HttpRequestData httpRequestData)
		{
			try
			{
				var entity = await GetFromBody<Entity>(httpRequestData);
				return await Service.Incluir(entity);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "Update")]
		[OpenApiOperation(ModelName + "Update", ModelName, Summary = "Atualiza o " + ModelName, Description = "Use para atualiza o " + ModelName)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + ModelName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> Update([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				var entity = await GetFromBody<Entity>(httpRequestData);
				if (entity.Id != id)
					throw new Exception("Id informado no path é diferente do id do objeto enviado no Body");

				return await Service.Alterar(entity);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "Delete")]
		[OpenApiOperation(ModelName + "Delete", ModelName, Summary = "Apaga um " + ModelName, Description = "Use para apagar o " + ModelName)]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Entity), Description = "Json contendo os dados do " + ModelName, Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity), Description = "OK response")]
		public async Task<dynamic> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				var entity = new Entity { Id = id };
				return await Service.Excluir(entity);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}
	}
}