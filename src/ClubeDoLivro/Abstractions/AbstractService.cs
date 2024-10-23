using ClubeDoLivro.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Abstractions
{
	public abstract class AbstractService<TEntity> : IService<TEntity>
	{
		protected readonly IServiceProvider ServiceProvider;
		protected readonly IRepository<TEntity> Repository;

		protected TService GetService<TService>() => ServiceProvider.GetRequiredService<TService>();

		protected AbstractService(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
			Repository = GetService<IRepository<TEntity>>();
		}

		public async Task<TEntity> ObterPor(int id)
		{
			return await Repository.ObterPor(id);
		}

		public async Task<IEnumerable<TEntity>> ObterTodos()
		{
			return await Repository.ObterTodos();
		}

		public async Task<TEntity> Incluir(TEntity entity)
		{
			return await Repository.Incluir(entity);
		}

		public async Task<TEntity> Alterar(TEntity entity)
		{
			return await Repository.Alterar(entity);
		}

		public async Task<TEntity> Excluir(TEntity entity)
		{
			return await Repository.Excluir(entity);
		}
	}
}