using ClubeDoLivro.Domains;

namespace ClubeDoLivro.Services
{
	public class AutorService
	{
		private readonly static List<Autor> _autores = [
			new Autor { Id = 1, Nome = "Bruno", Sobrenome = "Fernandes" },
			new Autor { Id = 2, Nome = "Walmir", Sobrenome = "Oliveira" },
			new Autor { Id = 3, Nome = "Ricardo", Sobrenome = "Castello"},
			new Autor { Id = 4, Nome = "Glauber", Sobrenome = "Lucas"   },
		];

		public async Task<Autor> Alterar(Autor autor)
		{
			var autorExistente = _autores.FirstOrDefault(x => x.Id == autor.Id);

			autorExistente.Nome = autor.Nome;
			autorExistente.Sobrenome = autor.Sobrenome;

			return await Task.FromResult(autorExistente);
		}

		public async Task<Autor> Excluir(int id)
		{
			var autor = _autores.FirstOrDefault(x => x.Id == id);
			_autores.Remove(autor);
			return await Task.FromResult(autor);
		}

		public async Task<Autor> Incluir(Autor autor)
		{
			autor.Id = _autores.Max(a => a.Id) + 1;
			_autores.Add(autor);
			return await Task.FromResult(autor);
		}

		public async Task<Autor> ObterPor(int id)
		{
			var autor = _autores.FirstOrDefault(x => x.Id == id);
			return await Task.FromResult(autor);
		}

		public async Task<IEnumerable<Autor>> ObterTodos()
		{
			return await Task.FromResult(_autores);
		}
	}
}
