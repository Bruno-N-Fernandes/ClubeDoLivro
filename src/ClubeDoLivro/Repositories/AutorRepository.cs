using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Repositories
{
	public class AutorRepository : AbstractRepository<Autor>
	{
		public AutorRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
	}
}