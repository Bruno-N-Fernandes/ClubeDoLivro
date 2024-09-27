using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClubeDoLivro.Function.Abstractions
{
	public abstract class AbstractController
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly ILogger Logger;

        protected TService GetService<TService>() => ServiceProvider.GetRequiredService<TService>();

        protected AbstractController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = GetService<ILogger>();
        }

		protected TValue GetValueFromHeader<TValue>(HttpRequestData httpRequestData, string headerName) => httpRequestData.GetValueFromHeader<TValue>(headerName);

		protected async Task<TValue> GetFromBody<TValue>(HttpRequestData httpRequestData) => await httpRequestData.GetObjectFromBody<TValue>();

	}
}