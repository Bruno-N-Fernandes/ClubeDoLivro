using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ClubeDoLivro.Abstractions
{
	public class QueryBuilder : IQueryBuilder
	{
		public string BuildSelectById<TEntity>(IDbConnection connection)
		{
			var cmdSql = BuildSelect<TEntity>(connection);
			return $"{cmdSql} Where (Id = @Id);";
		}

		public string BuildSelectAll<TEntity>(IDbConnection connection)
		{
			var cmdSql = BuildSelect<TEntity>(connection);
			return $"{cmdSql};";
		}

		private string BuildSelect<TEntity>(IDbConnection connection)
		{
			var tableName = GetTableName<TEntity>();
			return $"Select * From {tableName}";
		}

		public string BuildInsert<TEntity>(IDbConnection connection)
		{
			var tableName = GetTableName<TEntity>();
			var properties = GetProperties<TEntity>();
			var fields = string.Join(", ", properties);
			var values = string.Join(", ", properties.Select(x => $"@{x}"));
			var selectAutoIncField = "Select Last_Insert_RowId() As Id";
			return $"Insert Into {tableName} ({fields}) Values ({values}); {selectAutoIncField};";
		}

		public string BuildUpdate<TEntity>(IDbConnection connection)
		{
			var tableName = GetTableName<TEntity>();
			var properties = GetProperties<TEntity>();
			var updates = string.Join(", ", properties.Select(x => $"{x} = @{x}"));
			return $"Update {tableName} Set {updates} Where (Id = @Id);";
		}

		public string BuildDeleteById<TEntity>(IDbConnection connection)
		{
			var tableName = GetTableName<TEntity>();
			return $"Delete From {tableName} Where (Id = @Id);";
		}

		private string GetTableName<TEntity>() => typeof(TEntity).Name;

		private IEnumerable<string> GetProperties<TEntity>()
		{
			var type = typeof(TEntity);
			return type.GetProperties()
				.Where(p => p.CanRead && p.CanWrite)
				.Where(p => !p.Name.Equals("Id"))
				.Where(p => !IsGenericList(p.PropertyType))
				.Select(p => p.Name)
				.ToArray();
		}

		public static bool IsGenericList(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IList));
		}
	}
}