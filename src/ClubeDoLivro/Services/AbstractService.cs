using ClubeDoLivro.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Services
{
	public abstract class AbstractService<TEntity> : IService<TEntity>
	{
		protected readonly IRepository<TEntity> _repository;

		protected AbstractService(IServiceProvider serviceProvider)
		{
			_repository = serviceProvider.GetService<IRepository<TEntity>>();
		}

		public async Task<TEntity> ObterPor(int id)
		{
			return await _repository.ObterPor(id);
		}

		public async Task<IEnumerable<TEntity>> ObterTodos()
		{
			return await _repository.ObterTodos();
		}

		public async Task<TEntity> Incluir(TEntity autor)
		{
			return await _repository.Incluir(autor);
		}

		public async Task<TEntity> Alterar(TEntity autor)
		{
			return await _repository.Alterar(autor);
		}

		public async Task<TEntity> Excluir(TEntity autor)
		{
			return await _repository.Excluir(autor);
		}
	}
}