using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeDoLivro.Repositories
{
	public class AutorRepository : IRepository<Autor>
	{
		private readonly IDbConnection _connection;

		private readonly static List<Autor> _autores = [
			new Autor { Id = 1, Nome = "Bruno", Sobrenome = "Fernandes" },
		];

		public AutorRepository(IServiceProvider serviceProvider)
		{
			_connection = serviceProvider.GetService<IDbConnection>();
		}

		public async Task<Autor> ObterPor(int id)
		{
			var autor = _autores.FirstOrDefault(x => x.Id == id);
			return await Task.FromResult(autor);
		}

		public async Task<IEnumerable<Autor>> ObterTodos()
		{
			var query = "Select * From Autor";
			return await _connection.QueryAsync<Autor>(query);
		}

		public async Task<Autor> Incluir(Autor autor)
		{
			var query = "Insert Into Autor (Nome, Sobrenome) Values (@nome, @sobrenome)";
			var result = await _connection.ExecuteAsync(query, new { @nome = autor.Nome, @sobrenome = autor.Sobrenome });
			return autor;
		}

		public async Task<Autor> Alterar(Autor autor)
		{
			var autorExistente = _autores.FirstOrDefault(x => x.Id == autor.Id);

			autorExistente.Nome = autor.Nome;
			autorExistente.Sobrenome = autor.Sobrenome;

			return await Task.FromResult(autorExistente);
		}

		public async Task<Autor> Excluir(Autor autor)
		{
			var autorExistente = _autores.FirstOrDefault(x => x.Id == autor.Id);
			_autores.Remove(autor);
			return await Task.FromResult(autor);
		}
	}
}