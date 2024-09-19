using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
    }
}