using ClubeDoLivro.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ClubeDoLivro.Abstractions.Queries
{
	public abstract class QueryBuilder<TClass> : IQueryBuilder<TClass>
	{
		protected string TableName { get; set; }
		protected Column PrimaryKey { get; set; }
		private Column[] SelectColumns { get; set; }
		private Column[] OtherColumns { get; set; }
		private IDialect Dialect { get; }

		public virtual string CmdSqlDropTable => $"Drop Table If Exists {TableName};";
		public virtual string CmdSqlCreateTable => $"Create Table {TableName}(\r\n\t{PrimaryKey.Name} {Dialect.GetPrimaryKeyTemplate(TableName)},\r\n\t{string.Join(",\r\n\t", OtherColumns.Select(c => c.ToSql()))}\r\n);";
		public virtual string CmdSqlSelectAll => $"Select {string.Join(", ", SelectColumns.Select(c => c.ColumnWithAlias))} From {TableName} ";
		public virtual string CmdSqlSelectById => $"{CmdSqlSelectAll} Where ({PrimaryKey.Name} = @{PrimaryKey.Alias}) ";
		public virtual string CmdSqlInsert => $"Insert Into {TableName} ({string.Join(", ", OtherColumns.Select(c => c.Name))}) Values ({string.Join(", ", OtherColumns.Select(c => $"@{c.Alias}"))}); {Dialect.GetCmdSqlLastId()}; ";
		public virtual string CmdSqlUpdate => $"Update {TableName} Set {string.Join(", ", OtherColumns.Select(c => $"{c.Name} = @{c.Alias}"))} Where ({PrimaryKey.Name} = @{PrimaryKey.Alias}) ";
		public virtual string CmdSqlDeleteAll => $"Delete From {TableName} ";
		public virtual string CmdSqlDeleteById => $"{CmdSqlDeleteAll} Where ({PrimaryKey.Name} = @{PrimaryKey.Alias}) ";
		public virtual string GetCmdSqlSelectBy(string where = "") => CmdSqlSelectAll + where;

		protected QueryBuilder(IDialect dialect) => Dialect = dialect;

		protected TableBuilder<TClass> For(string tableName = null, string primaryKeyColumn = "Id") => new TableBuilder<TClass>(Dialect, tableName, primaryKeyColumn);

		internal void Build(string tableName, string primaryKeyColumn, IEnumerable<Column> columns)
		{
			TableName = tableName;
			PrimaryKey = columns.First(c => c.Name == primaryKeyColumn);
			SelectColumns = columns.ToArray();
			OtherColumns = columns.Where(c => c.Name != primaryKeyColumn).ToArray();
		}
	}

	public class TableBuilder<TClass>
	{
		private readonly List<Column> Columns = [];
		private readonly IDialect Dialect;
		private readonly string Table;
		private readonly string PrimaryKeyColumn;

		internal TableBuilder(IDialect dialect, string table, string primaryKeyColumn)
		{
			Dialect = dialect;
			Table = table ?? typeof(TClass).Name;
			PrimaryKeyColumn = primaryKeyColumn ?? "Id";
		}

		public TableBuilder<TClass> Add(Expression<Func<TClass, object>> expression, string columnName, int? lenght = null, int? precision = null)
		{
			var operand = (expression.Body as UnaryExpression)?.Operand ?? expression.Body;
			var lambda = operand.ToString();
			var alias = lambda.Substring(lambda.IndexOf(".") + 1);

			Columns.Add(new Column(columnName, alias.Replace(".", ""), operand.Type, lenght, precision));

			return this;
		}

		public TableBuilder<TClass> Add(Expression<Func<TClass, object>> expression, int? lenght = null, int? precision = null)
		{
			var operand = (expression.Body as UnaryExpression)?.Operand ?? expression.Body;
			var lambda = operand.ToString();
			var columnName = lambda.Substring(lambda.IndexOf(".") + 1);

			Columns.Add(new Column(columnName.Replace(".", ""), null, operand.Type, lenght, precision));

			return this;
		}

		public void Build(QueryBuilder<TClass> query)
		{
			foreach (var column in Columns)
				column.Build(Dialect);

			query.Build(Table, PrimaryKeyColumn, Columns);
		}
	}

	public class Column
	{
		public readonly string Name;
		public readonly string Alias;
		public readonly string ColumnWithAlias;
		private readonly Type _type;
		private readonly int? _length;
		private readonly int? _precision;
		private string _sqlType;

		internal Column(string columnName, string alias, Type type, int? length, int? precision)
		{
			Name = columnName ?? alias;
			Alias = alias ?? columnName;
			ColumnWithAlias = Name.Equals(Alias) ? Name : $"{Name} As {Alias}";
			_type = type;
			_length = length;
			_precision = precision;
		}

		internal void Build(IDialect dialect)
		{
			_sqlType = dialect.ConvertType(_type, _length?.ToString(), _precision?.ToString());
		}

		internal string ToSql() => $"{Name} {_sqlType}";
	}
}
