using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Repositories
{
	public class LivroRepository : IRepository<Livro>
	{
		public Task<Livro> Alterar(Livro entity)
		{
			throw new NotImplementedException();
		}

		public Task<Livro> Excluir(Livro entity)
		{
			throw new NotImplementedException();
		}

		public Task<Livro> Incluir(Livro entity)
		{
			throw new NotImplementedException();
		}

		public Task<Livro> ObterPor(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Livro>> ObterTodos()
		{
			throw new NotImplementedException();
		}
	}
}