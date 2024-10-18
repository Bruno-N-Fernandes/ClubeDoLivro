using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Services
{
	public class AutorService : AbstractService<Autor>
	{
		public AutorService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}