using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Services
{
	public class LivroService : AbstractService<Livro>
	{
		public LivroService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}