using ClubeDoLivro.Domains;
using ClubeDoLivro.Function.Abstractions;
using ClubeDoLivro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ClubeDoLivro.Function.Controllers
{
	public class AutorController : AbstractController
	{
		private const string ModelName = "Autor";
		public AutorService AutorService { get; }

		public AutorController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			AutorService = GetService<AutorService>();
		}

		[Function(ModelName + "GetAll")]
		[OpenApiOperation(ModelName + "GetAll", ModelName, Summary = "Listar todos os autores", Description = "Use para Listar todos os Autores")]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Autor), Description = "OK response")]
		public async Task<dynamic> GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = ModelName)] HttpRequestData httpRequestData)
		{
			try
			{
				return await AutorService.ObterTodos();
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "GetOne")]
		[OpenApiOperation(ModelName + "GetOne", ModelName, Summary = "Listar todos os autores", Description = "Use para Listar todos os Autores")]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Autor), Description = "OK response")]
		public async Task<dynamic> GetOne([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				return await AutorService.ObterPor(id);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}


		[Function(ModelName + "Create")]
		[OpenApiOperation(ModelName + "Create", ModelName, Summary = "Cria um novo autor", Description = "Use para criar um novo Autor")]
		[OpenApiRequestBody("application/json", typeof(Autor), Description = "Json contendo os dados do Autor", Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Autor), Description = "OK response")]
		public async Task<dynamic> Create([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = ModelName)] HttpRequestData httpRequestData)
		{
			try
			{
				var autor = await GetFromBody<Autor>(httpRequestData);
				return await AutorService.Incluir(autor);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "Update")]
		[OpenApiOperation(ModelName + "Update", ModelName, Summary = "Atualiza o autor", Description = "Use para atualiza o Autor")]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Autor), Description = "Json contendo os dados do Autor", Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Autor), Description = "OK response")]
		public async Task<dynamic> Update([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				var autor = await GetFromBody<Autor>(httpRequestData);
				if (autor.Id != id)
					throw new Exception("Id informado no path é diferente do id do objeto enviado no Body");

				return await AutorService.Alterar(autor);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}

		[Function(ModelName + "Delete")]
		[OpenApiOperation(ModelName + "Delete", ModelName, Summary = "Apaga um autor", Description = "Use para apagar o Autor")]
		[OpenApiParameter("id", In = ParameterLocation.Path)]
		[OpenApiRequestBody("application/json", typeof(Autor), Description = "Json contendo os dados do Autor", Required = true)]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Autor), Description = "OK response")]
		public async Task<dynamic> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = ModelName + "/{id}")] HttpRequestData httpRequestData, int id)
		{
			try
			{
				var autor = new Autor { Id = id };
				return await AutorService.Excluir(autor);
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}
	}
}