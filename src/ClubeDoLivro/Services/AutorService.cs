using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Services
{
	public class AutorService
	{
		public readonly AutorRepository _autorRepository;

		public AutorService(IServiceProvider serviceProvider)
		{
			_autorRepository = serviceProvider.GetService<AutorRepository>();
		}

		public async Task<Autor> ObterPor(int id)
		{
			return await _autorRepository.ObterPor(id);
		}

		public async Task<IEnumerable<Autor>> ObterTodos()
		{
			return await _autorRepository.ObterTodos();
		}

		public async Task<Autor> Incluir(Autor autor)
		{
			return await _autorRepository.Incluir(autor);
		}

		public async Task<Autor> Alterar(Autor autor)
		{
			return await _autorRepository.Alterar(autor);
		}

		public async Task<Autor> Excluir(Autor autor)
		{
			return await _autorRepository.Excluir(autor);
		}
	}











	public abstract class AbstractService<TEntity> : IService<TEntity>
	{
		public readonly IRepository<TEntity> _autorRepository;

		public AbstractService(IServiceProvider serviceProvider)
		{
			_autorRepository = serviceProvider.GetService<IRepository<TEntity>>();
		}

		public async Task<TEntity> ObterPor(int id)
		{
			return await _autorRepository.ObterPor(id);
		}

		public async Task<IEnumerable<TEntity>> ObterTodos()
		{
			return await _autorRepository.ObterTodos();
		}

		public async Task<TEntity> Incluir(TEntity autor)
		{
			return await _autorRepository.Incluir(autor);
		}

		public async Task<TEntity> Alterar(TEntity autor)
		{
			return await _autorRepository.Alterar(autor);
		}

		public async Task<TEntity> Excluir(TEntity autor)
		{
			return await _autorRepository.Excluir(autor);
		}
	}
}