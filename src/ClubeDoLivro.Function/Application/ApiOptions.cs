using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace ClubeDoLivro.Function.Application
{
	/// <summary>
	/// https://techcommunity.microsoft.com/t5/apps-on-azure-blog/azure-functions-openapi-extension-update/ba-p/3702128
	/// https://blog.angelwebdesigns.com.au/changing-azure-function-to-isolated/
	/// https://www.c-sharpcorner.com/article/secure-serverless-azure-functions-using-jwt-auth-and-c-sharp-net-6/
	/// https://joonasw.net/view/azure-ad-jwt-authentication-in-net-isolated-process-azure-functions
	/// https://learn.microsoft.com/pt-br/azure/azure-functions/dotnet-isolated-process-guide
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "Documentation")]
	public class ApiOptions : OpenApiConfigurationOptions
	{
		public ApiOptions()
		{
			OpenApiVersion = OpenApiVersionType.V3;
			IncludeRequestingHostName = true;
			Info = OpenApiInfo;
			ForceHttp = false;
			ForceHttps = false;
			Servers = OpenApiServers;
		}

		private List<OpenApiServer> OpenApiServers => new List<OpenApiServer>
		{
			new OpenApiServer { Description = "DEV", Url = "http://localhost:7071/api/" },
			new OpenApiServer { Description = "HMG", Url = "https://api-hmg.clubeDoLivro.com.br/api" },
			new OpenApiServer { Description = "PRD", Url = "https://api.clubeDoLivro.com.br/api" },
		};

		private OpenApiInfo OpenApiInfo => new OpenApiInfo
		{
			Version = "1.0.0.0",
			Title = "Clube do Livro",
			Description = "Sistema de apoio ao controle, organização de empréstimos de livros",
			TermsOfService = new Uri("https://loja.wisementoring.com.br"),
			Contact = new OpenApiContact()
			{
				Name = "Bruno Fernandes",
				Email = "clubeDoLivro@wisementoring.com.br",
				Url = new Uri("https://loja.wisementoring.com.br"),
			},
			License = new OpenApiLicense()
			{
				Name = "MIT",
				Url = new Uri("https://loja.wisementoring.com.br"),
			},
			Extensions = new Dictionary<string, IOpenApiExtension>
			{
			},
		};
	}
}