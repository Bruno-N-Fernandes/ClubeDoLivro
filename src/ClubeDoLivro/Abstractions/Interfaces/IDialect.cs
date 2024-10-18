using System;

namespace ClubeDoLivro.Abstractions.Interfaces
{
	public interface IDialect
	{
		string ConvertType(Type propType, string lenght, string precision);
		string GetCmdSqlLastId();
		string GetNullable(bool isNull);
		string GetPrimaryKeyTemplate(string tabela);
	}
}