using ClubeDoLivro.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Abstractions
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

		public async Task<TEntity> Incluir(TEntity entity)
		{
			return await _repository.Incluir(entity);
		}

		public async Task<TEntity> Alterar(TEntity entity)
		{
			return await _repository.Alterar(entity);
		}

		public async Task<TEntity> Excluir(TEntity entity)
		{
			return await _repository.Excluir(entity);
		}
	}
}