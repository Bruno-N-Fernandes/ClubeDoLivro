using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Repositories
{
	public class LivroRepository : AbstractRepository<Livro>
	{
        public LivroRepository(IServiceProvider serviceProvider): base(serviceProvider) { }
    }
}