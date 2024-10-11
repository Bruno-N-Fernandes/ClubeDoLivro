using ClubeDoLivro.Abstractions;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ClubeDoLivro.Repositories
{
	public class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
	{
		protected readonly IDbConnection _connection;
		protected readonly IQueryBuilder _querybuilder;

		public AbstractRepository(IServiceProvider serviceProvider)
		{
			_connection = serviceProvider.GetService<IDbConnection>();
			_querybuilder = serviceProvider.GetService<IQueryBuilder>();
		}

		public async Task<TEntity> ObterPor(int id)
		{
			var cmdSql = _querybuilder.BuildSelectById<TEntity>(_connection);
			return await _connection.QuerySingleOrDefaultAsync<TEntity>(cmdSql);
		}

		public async Task<IEnumerable<TEntity>> ObterTodos()
		{
			var cmdSql = _querybuilder.BuildSelectAll<TEntity>(_connection);
			return await _connection.QueryAsync<TEntity>(cmdSql);
		}

		public async Task<TEntity> Incluir(TEntity entity)
		{
			var cmdSql = _querybuilder.BuildInsert<TEntity>(_connection);
			entity.Id = await _connection.ExecuteScalarAsync<int>(cmdSql, entity);
			return entity;
		}

		public async Task<TEntity> Alterar(TEntity entity)
		{
			var cmdSql = _querybuilder.BuildUpdate<TEntity>(_connection);
			var result = await _connection.ExecuteAsync(cmdSql, entity);
			return (result == 1) ? entity : null;
		}

		public async Task<TEntity> Excluir(TEntity entity)
		{
			var cmdSql = _querybuilder.BuildDeleteById<TEntity>(_connection);
			var result = await _connection.ExecuteAsync(cmdSql, entity);
			return (result == 1) ? entity : null;
		}
	}
}