using ClubeDoLivro.Abstractions.Interfaces;
using System;
using System.Text;
using System.Xml;

namespace ClubeDoLivro.Abstractions.Queries.Dialects
{
    public abstract class Dialect : IDialect
	{
		protected abstract string PrimaryKey { get; }
		protected abstract string AutoIncrement { get; }
		protected abstract string GetLastId { get; }
		protected abstract string NotNull { get; }
		protected abstract string TypeChar { get; }
		protected abstract string TypeBoolean { get; }
		protected abstract string TypeInt16 { get; }
		protected abstract string TypeInt32 { get; }
		protected abstract string TypeInt64 { get; }
		protected abstract string TypeDecimal { get; }
		protected abstract string TypeDateOnly { get; }
		protected abstract string TypeDateTime { get; }
		protected abstract string TypeTime { get; }
		protected abstract string TypeTimeStamp { get; }
		protected abstract string TypeGuid { get; }
		protected abstract string TypeString { get; }
		protected abstract string TypeXML { get; }
		protected abstract string TypeText { get; }
		protected abstract string TypeDefault { get; }

		public abstract string GetPrimaryKeyTemplate(string tabela);

		protected virtual string DoConvertType(Type type) => TypeDefault;

		public virtual string GetCmdSqlLastId() => GetLastId;

		public virtual string GetNullable(bool isNull) => isNull ? string.Empty : NotNull;

		public virtual string ConvertType(Type propType, string lenght, string precision)
		{
			var rootType = Nullable.GetUnderlyingType(propType);
			var isNull = " " + GetNullable(rootType != null);
			var type = rootType ?? propType;

			if (type == typeof(string))
				return TypeString.Replace("{Length}", lenght ?? "250") + isNull;
			else if (type == typeof(char))
				return TypeChar + isNull;
			else if (type == typeof(bool))
				return TypeBoolean + isNull;
			else if (type.IsEnum)
				return TypeInt16 + isNull;
			else if (type == typeof(short))
				return TypeInt16 + isNull;
			else if (type == typeof(int))
				return TypeInt32 + isNull;
			else if (type == typeof(long))
				return TypeInt64 + isNull;
			else if (type == typeof(float))
				return TypeDecimal.Replace("{Length}", lenght ?? "6").Replace("{Precision}", precision ?? "2") + isNull;
			else if (type == typeof(decimal))
				return TypeDecimal.Replace("{Length}", lenght ?? "13").Replace("{Precision}", precision ?? "3") + isNull;
			else if (type == typeof(double))
				return TypeDecimal.Replace("{Length}", lenght ?? "20").Replace("{Precision}", precision ?? "6") + isNull;
			else if (type == typeof(DateOnly))
				return TypeDateOnly + isNull;
			else if (type == typeof(DateTime))
				return TypeDateTime + isNull;
			else if (type == typeof(TimeSpan))
				return TypeTime + isNull;
			else if (type == typeof(Guid))
				return TypeGuid + isNull;
			else if (type == typeof(StringBuilder))
				return TypeText + isNull;
			else if (type == typeof(XmlNode))
				return TypeXML + isNull;
			else if (type.Implements<IEntity>())
				return TypeInt32 + isNull;
			else if (type.Implements<IEntity>())
				return TypeInt64 + isNull;

			return DoConvertType(type);
		}
	}
}