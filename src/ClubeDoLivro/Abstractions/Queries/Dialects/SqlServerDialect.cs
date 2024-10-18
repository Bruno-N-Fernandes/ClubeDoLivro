namespace ClubeDoLivro.Abstractions.Queries.Dialects
{
	public class SqlServerDialect : Dialect
	{
		protected override string PrimaryKey => "Primary Key";
		protected override string AutoIncrement => "Identity(1, 1)";
		protected override string GetLastId => "Select Scope_Identity()";
		protected override string NotNull => "Not Null";
		protected override string TypeChar => "Char(1)";
		protected override string TypeBoolean => "Bit";
		protected override string TypeInt16 => "SmallInt";
		protected override string TypeInt32 => "Int";
		protected override string TypeInt64 => "BigInt";
		protected override string TypeDecimal => "Decimal({Length}, {Precision})";
		protected override string TypeDateOnly => "Date";
		protected override string TypeDateTime => "DateTime";
		protected override string TypeTime => "Time";
		protected override string TypeTimeStamp => "TimeStamp";
		protected override string TypeGuid => "UniqueIdentifier";
		protected override string TypeString => "VarChar({Length})";
		protected override string TypeXML => "XML";
		protected override string TypeText => "Text";
		protected override string TypeDefault => "VarChar(4000)";

		public override string GetPrimaryKeyTemplate(string tabela) => $"{TypeInt64} {NotNull} {AutoIncrement} Constraint PK_{tabela} {PrimaryKey}";
	}
}