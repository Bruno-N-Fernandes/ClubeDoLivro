using ClubeDoLivro.Function.Abstractions;
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

		public AutorController(IServiceProvider serviceProvider) : base(serviceProvider)
		{

		}

		[Function(ModelName + "GetAll")]
		[OpenApiOperation(ModelName + "GetAll", ModelName, Summary = "Listar todos os autores", Description = "Use para Listar todos os Autores")]
		[OpenApiResponseWithBody(HttpStatusCode.Unauthorized, "application/json", typeof(Message), Description = "Unauthorized response")]
		[OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(Message), Description = "BadRequest response")]
		[OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(Message), Description = "NotFound response")]
		[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string), Description = "OK response")]
		public async Task<dynamic> GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "Get", Route = ModelName)] HttpRequestData httpRequestData)
		{
			try
			{
				var json = new[] {
					new { Id = 1, Nome = "Bruno", Sobrenome = "Fernandes", Livros = new object[0] },
					new { Id = 2, Nome = "Walmir", Sobrenome = "Oliveira", Livros = new object[0] },
					new { Id = 3, Nome = "Ricardo", Sobrenome = "Castello", Livros = new object[0] },
					new { Id = 4, Nome = "Glauber", Sobrenome = "Lucas", Livros = new object[0] },
				};
				return json;
			}
			catch (Exception exception)
			{
				return await Task.FromResult(new BadRequestObjectResult(new Message(exception.Message)));
			}
		}
	}
}