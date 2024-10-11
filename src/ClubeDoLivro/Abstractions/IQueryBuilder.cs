using System.Data;

namespace ClubeDoLivro.Abstractions
{
	public interface IQueryBuilder
	{
		string BuildSelectAll<TEntity>(IDbConnection connection);
		string BuildSelectById<TEntity>(IDbConnection connection);
		string BuildInsert<TEntity>(IDbConnection connection);
		string BuildUpdate<TEntity>(IDbConnection connection);
		string BuildDeleteById<TEntity>(IDbConnection connection);
	}
}