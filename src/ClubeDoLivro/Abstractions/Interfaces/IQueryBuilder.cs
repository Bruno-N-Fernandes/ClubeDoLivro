namespace ClubeDoLivro.Abstractions.Interfaces
{
	public interface IQueryBuilder
	{
		string CmdSqlDropTable { get; }
		string CmdSqlSelectAll { get; }
		string CmdSqlSelectById { get; }
		string CmdSqlInsert { get; }
		string CmdSqlUpdate { get; }
		string CmdSqlDeleteAll { get; }
		string CmdSqlDeleteById { get; }
		string GetCmdSqlSelectBy(string where);
	}

	public interface IQueryBuilder<TEntity> : IQueryBuilder
	{
		string CmdSqlCreateTable { get; }
	}
}